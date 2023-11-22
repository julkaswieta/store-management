using Microsoft.EntityFrameworkCore;

namespace OffersApi.Models;

public class OfferContext : DbContext
{
    public OfferContext(DbContextOptions<OfferContext> options) : base(options)
    {
    }

    public DbSet<Offer> Offers { get; set; } = null!;
}