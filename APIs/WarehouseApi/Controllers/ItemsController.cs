using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Models;

namespace WarehouseApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository repository;

    public ItemsController(IItemRepository repository)
    {
        this.repository = repository;
    }

    // GET: /Items
    [HttpGet]
    public IActionResult GetItems()
    {
        return Ok(repository.GetItems());
    }
}