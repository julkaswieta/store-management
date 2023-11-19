using InventoryControl.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryControl;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        BuildService(builder);
        var app = builder.Build();
        SetupApp(app);

        // this task is responsible for polling the warehouse api for updates 
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
        using (var scope = scopedFactory.CreateScope())
        {
            var scopedConnection = scope.ServiceProvider.GetRequiredService<InventoryController>();
            Task task = Task.Run(scopedConnection.MonitorStock);
        }

        app.Run();
    }

    private static void BuildService(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepository, Repository>();
        builder.Services.AddTransient<InventoryController>();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ItemContext>(options =>
            options.UseInMemoryDatabase("Items"));
        builder.Services.AddDbContext<AlertContext>(options =>
            options.UseInMemoryDatabase("Alerts"));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void SetupApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
    }
}


