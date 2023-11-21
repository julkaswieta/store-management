using Microsoft.EntityFrameworkCore;

namespace EnablingApi.Models;

public class RequestContext : DbContext
{
    public RequestContext(DbContextOptions<RequestContext> options)
        : base(options) { }

    public DbSet<Request> Requests { get; set; } = null!;
}