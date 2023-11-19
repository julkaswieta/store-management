using Microsoft.EntityFrameworkCore;

namespace PriceControlApi.Models;

public class OfferContext : DbContext
{
    public OfferContext(DbContextOptions<OfferContext> options) : base(options)
    {
    }

    public DbSet<Offer> Offers { get; set; } = null!;
}