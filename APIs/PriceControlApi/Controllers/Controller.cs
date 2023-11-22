using Microsoft.AspNetCore.Mvc;
using PriceControlApi.Database;
using PriceControlApi.Models;

namespace PriceControlApi.Controllers;

[ApiController]
public class Controller : ControllerBase
{
    private readonly IRepository repository;
    public Controller(IRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [Route("items")]
    public IActionResult GetItems()
    {
        return Ok(repository.GetItems());
    }

    [HttpGet]
    [Route("items/{id}")]
    public IActionResult GetItem(int id)
    {
        var item = repository.GetItems().Where(p => p.Id == id).FirstOrDefault();
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPut]
    [Route("items/{id}")]
    public IActionResult UpdateItem(int id, Item item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        var localItem = repository.GetItems().Where(p => p.Id == id).FirstOrDefault();

        if (localItem == null)
        {
            return NotFound();
        }
        else
        {
            repository.UpdateItem(item);
            return NoContent();
        }
    }
}