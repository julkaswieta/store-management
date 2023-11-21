using EnablingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnablingApi.Controllers;

[ApiController]
public class Controller : ControllerBase
{
    private readonly IRepository repository;
    public Controller(IRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [Route("requests")]
    public IActionResult GetRequests()
    {
        return Ok(repository.GetRequests());
    }

    [HttpPut]
    [Route("requests/{id}")]
    public IActionResult UpdateRequest(int id, Request request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        var localRequest = repository.GetRequests().Where(p => p.Id == id).FirstOrDefault();
        if (localRequest == null)
        {
            return NotFound();
        }
        else
        {
            repository.UpdateRequest(request);
            return NoContent();
        }
    }
}