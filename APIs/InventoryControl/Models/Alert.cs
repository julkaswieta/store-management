namespace InventoryControl.Models;

public class Alert
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public required string DateTriggered { get; set; }
}