using PriceControlApi.Models;

namespace PriceControlApi.Database;

public interface IRepository
{
    public IEnumerable<Item> GetItems();

    public void UpdateItem(Item item);
}