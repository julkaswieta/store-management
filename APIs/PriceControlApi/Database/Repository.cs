using System.Text.Json;
using PriceControlApi.Models;

namespace PriceControlApi.Database;

public class Repository : IRepository
{
    private readonly ItemContext itemContext;

    public Repository(ItemContext itemContext)
    {
        this.itemContext = itemContext;
    }

    public IEnumerable<Item> GetItems()
    {
        LoadItems();
        return itemContext.Items.OrderBy(p => p.Id).ToList();
    }

    public void UpdateItem(Item item)
    {
        LoadItems();
        itemContext.Remove(itemContext.Items.Find(item.Id));
        itemContext.Items.Add(item);
        itemContext.SaveChanges();
        SaveItems();
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

    private void SaveItems()
    {
        using (StreamWriter writer = new StreamWriter("./Data/items.json"))
        {
            var items = JsonSerializer.Serialize<List<Item>>(itemContext.Items.ToList());
            writer.Write(items);
        }
    }
}