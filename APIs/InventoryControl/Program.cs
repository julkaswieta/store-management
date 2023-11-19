using InventoryControl.Database;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl;
class Program
{
    private static InventoryController controller = InventoryController.Instance;
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        BuildService(builder);
        var app = builder.Build();
        SetupApp(app);

        // this task is responsible for polling the warehouse api for updates 
        Task task = Task.Run(controller.MonitorStock);

        app.Run();
    }

    private static void BuildService(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepository, Repository>();
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


