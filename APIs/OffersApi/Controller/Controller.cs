using Microsoft.AspNetCore.Mvc;
using OffersApi.Models;

namespace OffersApi.Controllers;

[ApiController]
public class Controller : ControllerBase
{
    private readonly IRepository repository;

    public Controller(IRepository repository)
    {
        this.repository = repository;
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
    [Route("offers/{id}")]
    public IActionResult UpdateOffer(int id, [FromBody] Offer offer)
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
    public IActionResult CreateOffer([FromBody] Offer offer)
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