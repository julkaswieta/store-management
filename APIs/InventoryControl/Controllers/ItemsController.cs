using InventoryControl.Database;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.Controllers;

[ApiController]
[Route("inventory")]
public class ItemsController : ControllerBase
{
    private readonly IRepository repository;
    public ItemsController(IRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet] // GET: /inventory/items
    [Route("items")]
    public IActionResult GetItems()
    {
        return Ok(repository.GetItems());
    }

    [HttpGet] // GET: /inventory/alerts
    [Route("alerts")]
    public IActionResult GetAlerts()
    {
        return Ok(repository.GetAlerts());
    }
}