using System.ComponentModel.DataAnnotations;

namespace OffersApi.Models;

public class Offer
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required string Code { get; set; }
    public string ItemIds { get; set; }
    public bool LoyalCustomersOnly { get; set; }
}