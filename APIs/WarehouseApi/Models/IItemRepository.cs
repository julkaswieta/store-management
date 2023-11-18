namespace WarehouseApi.Models;

public interface IItemRepository
{
    public IEnumerable<Item> GetItems();
}