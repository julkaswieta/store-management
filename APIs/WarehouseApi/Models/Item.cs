namespace WarehouseApi.Models;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public required string Category { get; set; }
    public int DaysSinceLastOrder { get; set; }
}