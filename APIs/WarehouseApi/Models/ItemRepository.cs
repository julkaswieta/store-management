using System.Security.Cryptography;
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
        Random random = new Random();
        int listToLoad = random.Next(1, 4); // generate number between 1 and 3
        // clear out the current context
        context.Items.RemoveRange(context.Items);
        context.SaveChanges();
        string pathToRead = "./Data/items" + listToLoad + ".json";
        using (StreamReader reader = new StreamReader(pathToRead))
        {
            string jsonString = reader.ReadToEnd();
            var items = JsonSerializer.Deserialize<List<Item>>(jsonString);
            context.Items.AddRange(items);
            context.SaveChanges();
        }
    }
}