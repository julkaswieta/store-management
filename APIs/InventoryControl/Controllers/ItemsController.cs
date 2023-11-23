using InventoryControl.Database;
using InventoryControl.Models;
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

    [HttpPut]
    [Route("alerts/{id}")]
    public IActionResult UpdateAlert(int id, [FromBody] Alert alert)
    {
        if (alert == null || id != alert.Id)
        {
            return BadRequest();
        }

        var localAlert = repository.GetAlerts().Where(p => p.Id == id).FirstOrDefault();

        if (localAlert == null)
        {
            return NotFound();
        }
        else
        {
            repository.DeleteAlert(alert);
            return NoContent();
        }
    }
}