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

    [HttpGet]
    [Route("offers")]
    public IActionResult GetOffers()
    {
        return Ok(repository.GetOffers());
    }

    [HttpGet]
    [Route("offers/{id}")]
    public IActionResult GetOffer(int id)
    {
        var item = repository.GetOffers().Where(p => p.Id == id).FirstOrDefault();
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

    [HttpPut]
    [Route("offers/{id}")]
    public IActionResult UpdateOffer(int id, Offer offer)
    {
        if (id != offer.Id)
        {
            return BadRequest();
        }

        var localOffer = repository.GetOffers().Where(p => p.Id == id).FirstOrDefault();

        if (localOffer == null)
        {
            return NotFound();
        }
        else
        {
            repository.UpdateOffer(offer);
            return NoContent();
        }
    }

    [HttpPost]
    [Route("offers")]
    public IActionResult CreateOffer(Offer offer)
    {
        if (repository.GetOffers().FirstOrDefault(p => p.Id == offer.Id) != null)
        {
            return BadRequest();
        }

        repository.AddOffer(offer);
        return Ok(offer);
    }

    [HttpDelete]
    [Route("offers/{id}")]
    public IActionResult DeleteOffer(int id)
    {
        if (repository.GetOffers().FirstOrDefault(p => p.Id == id) == null)
        {
            return NotFound();
        }
        repository.DeleteOffer(id);
        return NoContent();
    }
}