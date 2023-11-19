using InventoryControl.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Database;

public class AlertContext : DbContext
{
    public AlertContext(DbContextOptions<AlertContext> options) : base(options)
    {
    }

    public DbSet<Alert> Alerts { get; set; } = null!;
}