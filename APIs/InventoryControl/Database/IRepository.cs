using InventoryControl.Models;

namespace InventoryControl.Database;

public interface IRepository
{
    public IEnumerable<Item> GetItems();
    public IEnumerable<Alert> GetAlerts();
    public void DeleteAlert(Alert alert);
}