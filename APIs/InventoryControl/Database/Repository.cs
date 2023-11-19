using System.Text.Json;
using InventoryControl.Models;

namespace InventoryControl.Database;

public class Repository : IRepository
{
    private readonly ItemContext itemContext;
    private readonly AlertContext alertContext;

    public Repository(ItemContext itemContext, AlertContext alertContext)
    {
        this.itemContext = itemContext;
        this.alertContext = alertContext;
    }

    public IEnumerable<Item> GetItems()
    {
        LoadItems();
        return itemContext.Items.OrderBy(p => p.Id).ToList();
    }

    public IEnumerable<Alert> GetAlerts()
    {
        LoadAlerts();
        return alertContext.Alerts.OrderBy(p => p.Id).ToList();
    }

    public void AddItem(Item item)
    {
        LoadItems();
        itemContext.Items.Add(item);
        itemContext.SaveChanges();
    }

    public void RemoveItem(int itemId)
    {
        LoadItems();
        itemContext.Remove(itemContext.Items.First(p => p.Id == itemId));
        itemContext.SaveChanges();
    }

    private void LoadItems()
    {
        itemContext.Items.RemoveRange(itemContext.Items);
        itemContext.SaveChanges();
        using (StreamReader reader = new StreamReader("./Data/items.json"))
        {
            string jsonString = reader.ReadToEnd();
            var items = JsonSerializer.Deserialize<List<Item>>(jsonString);
            itemContext.Items.AddRange(items);
            itemContext.SaveChanges();
        }
    }

    public void SaveItems()
    {
        using (StreamWriter writer = new StreamWriter("./Data/items.json"))
        {
            var jsonString = JsonSerializer.Serialize<List<Item>>(itemContext.Items.ToList());
            writer.Write(jsonString);
        }
    }

    public void UpdateOrderDate(int itemId)
    {
        itemContext.Items.First(p => p.Id == itemId).DaysSinceLastOrder = 0;
    }

    private void LoadAlerts()
    {
        alertContext.Alerts.RemoveRange(alertContext.Alerts);
        alertContext.SaveChanges();
        using (StreamReader reader = new StreamReader("./Data/alerts.json"))
        {
            string jsonString = reader.ReadToEnd();
            var alerts = JsonSerializer.Deserialize<List<Alert>>(jsonString);
            alertContext.Alerts.AddRange(alerts);
            alertContext.SaveChanges();
        }
    }

    public Item? FindItem(int itemId)
    {
        return itemContext.Items.FirstOrDefault(p => p.Id == itemId);
    }
}