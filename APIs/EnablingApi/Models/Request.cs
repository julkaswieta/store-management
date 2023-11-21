namespace EnablingApi.Models;

public class Request
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ItemId { get; set; }
    public decimal AmountRequested { get; set; }
    public required string Status { get; set; }
}