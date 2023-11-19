using Microsoft.AspNetCore.Mvc;
using PriceControlApi.Database;

namespace PriceControlApi.Controllers;

[ApiController]
[Route("pricecontrol")]
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
        return Ok(repository.GetItems().First(p => p.Id == id));
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
        return Ok(repository.GetOffers().First(p => p.Id == id));
    }
}