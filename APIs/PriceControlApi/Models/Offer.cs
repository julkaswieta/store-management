namespace PriceControlApi.Models;

public class Offer
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string Code { get; set; }
    public required int[] ItemIds { get; set; }
    public bool LoyalCustomersOnly { get; set; }
}