using Microsoft.AspNetCore.Mvc;
namespace CentralInventoryApi.Controllers;

[ApiController]
public class InventoryController : ControllerBase
{
    private string received = "Order received for item id ";
    public InventoryController() { }

    [HttpGet]
    [Route("request/{id}")]
    public IActionResult PostStockRequest(int id)
    {
        string response = received += id;
        return Ok(response);
    }
}