using System.Text.Json;
using InventoryControl.Models;

namespace InventoryControl;
public sealed class InventoryController
{
    private static readonly InventoryController instance = new InventoryController();

    private static readonly string warehouseApiUrl = "http://localhost:3001/items";
    // TODO: update to actual url
    private static readonly string centralApiUrl = "http://localhost:3002/request";

    private InventoryController() { }

    public static InventoryController Instance
    {
        get { return instance; }
    }

    public async void OrderFromHeadquarters(int itemId)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = await client.GetStringAsync(centralApiUrl);
                Console.WriteLine(response);
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions
                Console.WriteLine($"Exception: {ex.Message} {ex.StackTrace}");
            }
        }
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

                    Console.WriteLine(responseBody);
                    ProcessStockUpdate(responseBody);

                    Thread.Sleep(100); // wait 10 seconds for next update
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
        using (StreamWriter writer = new StreamWriter("./Data/items.json"))
        {
            writer.Write(stockUpdate);
        }

        var items = JsonSerializer.Deserialize<List<Item>>(stockUpdate);
        foreach (var item in items)
        {
            if (item.Quantity < 20)
            {
                OrderFromHeadquarters(item.Id);
            }
        }
    }
}