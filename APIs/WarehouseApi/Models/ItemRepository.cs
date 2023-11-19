using System.Text.Json;

namespace WarehouseApi.Models;

public class ItemRepository : IItemRepository
{
    private readonly ItemContext context;
    public ItemRepository(ItemContext context)
    {
        this.context = context;
    }

    public IEnumerable<Item> GetItems()
    {
        LoadItems();
        return context.Items.OrderBy(p => p.Id).ToList();
    }

    private void LoadItems()
    {
        context.Items.RemoveRange(context.Items);
        context.SaveChanges();
        using (StreamReader reader = new StreamReader("./Data/items.json"))
        {
            string jsonString = reader.ReadToEnd();
            var items = JsonSerializer.Deserialize<List<Item>>(jsonString);
            context.Items.AddRange(items);
            context.SaveChanges();
        }
    }
}