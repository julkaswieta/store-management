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
}