using InventoryControl.Models;

namespace InventoryControl.Database;

public interface IRepository
{
    public IEnumerable<Item> GetItems();
    public IEnumerable<Alert> GetAlerts();

    public void SaveItems();

    public void AddItem(Item item);

    public void RemoveItem(int itemId);

    public void UpdateOrderDate(int itemId);

    public Item? FindItem(int itemId);
}