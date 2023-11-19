using InventoryControl.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Database;

public class ItemContext : DbContext
{
    public ItemContext(DbContextOptions<ItemContext> options) : base(options)
    {
    }

    public DbSet<Item> Items { get; set; } = null!;
}