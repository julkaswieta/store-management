using Microsoft.AspNetCore.Mvc;
namespace CentralInventoryApi.Controllers;

[ApiController]
[Route("request")]
public class InventoryController : ControllerBase
{
    private string received = "Order received.";
    public InventoryController() { }

    [HttpGet]
    public IActionResult PostStockRequest()
    {
        return Ok(received);
    }
}