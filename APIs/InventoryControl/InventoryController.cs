using System.Text.Json;
using InventoryControl.Database;
using InventoryControl.Models;

namespace InventoryControl;
public sealed class InventoryController : IInventoryController
{
    private readonly IRepository repository;

    // private static readonly string warehouseApiUrl = "http://localhost:3001/items";
    // private static readonly string centralApiUrl = "http://localhost:3002/request";

    private static readonly string warehouseApiUrl = "http://host.docker.internal:3001/items";

    public InventoryController(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task MonitorStock()
    {
        while (true)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string responseBody = await client.GetStringAsync(warehouseApiUrl);

                    ProcessStockUpdate(responseBody);

                    Thread.Sleep(10000); // wait 10 seconds for next update
                }
                catch (HttpRequestException ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Exception: {ex.Message} {ex.StackTrace}");
                }
            }
        }
    }

    private void ProcessStockUpdate(string stockUpdate)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var upoloadedItems = JsonSerializer.Deserialize<List<Item>>(stockUpdate, options);
        List<Item> localItems = null;
        using (StreamReader reader = new StreamReader("./Data/items.json"))
        {
            var json = reader.ReadToEnd();
            localItems = JsonSerializer.Deserialize<List<Item>>(json, options);
        }

        foreach (var item in upoloadedItems)
        {
            var localItem = localItems.Find(p => p.Id == item.Id);
            // if it exists, update it
            if (localItem != null)
            {
                localItem.Quantity = item.Quantity;
                localItem.Name = item.Name;
                localItem.DaysSinceLastOrder = localItem.DaysSinceLastOrder;
                localItem.Category = item.Category;
            }
            // if it doesn't, add it to the list 
            else
            {
                localItem = item;
                localItems.Add(item);
            }
            if (item.Quantity < 100 && item.DaysSinceLastOrder > 3)
            {
                GenerateAlert(item.Id);
            }
        }

        // save it to the local db
        using (StreamWriter writer = new StreamWriter("./Data/items.json"))
        {
            var json = JsonSerializer.Serialize<List<Item>>(localItems);
            writer.Write(json);
        }
    }

    private async void GenerateAlert(int itemId)
    {
        try
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Alert> existingAlerts = null!;
            using (StreamReader reader = new StreamReader("./Data/alerts.json"))
            {

                var json = reader.ReadToEnd();
                try
                {
                    existingAlerts = JsonSerializer.Deserialize<List<Alert>>(json, options);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("no alerts");
                }
            }
            if (existingAlerts == null)
            {
                existingAlerts = new List<Alert>();
                Alert newAlert = new Alert
                {
                    Id = 1,
                    ItemId = itemId,
                    DateTriggered = DateTime.UtcNow.ToString()
                };
                existingAlerts.Add(newAlert);
            }
            else
            {
                var alertWithSameId = existingAlerts?.Find(p => p.ItemId == itemId);
                // create new alert 
                if (alertWithSameId == null)
                {
                    int biggestId = existingAlerts.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                    Alert newAlert = new Alert
                    {
                        Id = biggestId + 1,
                        ItemId = itemId,
                        DateTriggered = DateTime.UtcNow.ToString()
                    };
                    existingAlerts.Add(newAlert);
                }
            }

            // save them to the local db
            using (StreamWriter writer = new StreamWriter("./Data/alerts.json"))
            {
                if (existingAlerts != null)
                {
                    var json = JsonSerializer.Serialize<List<Alert>>(existingAlerts);
                    writer.Write(json);
                }
                else
                    writer.Write("");
            }
        }
        catch { }
    }
}