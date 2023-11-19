using Microsoft.EntityFrameworkCore;
using PriceControlApi.Database;
using PriceControlApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddDbContext<ItemContext>(options =>
    options.UseInMemoryDatabase("Items"));
builder.Services.AddDbContext<OfferContext>(options =>
    options.UseInMemoryDatabase("Offers"));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();