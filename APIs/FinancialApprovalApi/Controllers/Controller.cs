using System.Text;
using System.Text.Json;
using FinancialApprovalApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApprovalApi.Controllers;

[ApiController]
public class Controller : ControllerBase
{

    // private readonly string EnablingApiUrl = "http://localhost:3005/requests";
    private readonly string EnablingApiUrl = "http://host.docker.internal:3005/requests";


    [HttpGet]
    [Route("requests")]
    public async Task<IActionResult> GetRequests()
    {
        using (HttpClient client = new HttpClient())
        {
            string responseBody = await client.GetStringAsync(EnablingApiUrl);
            return Ok(responseBody);
        }
    }

    [HttpPut]
    [Route("requests/{id}")]
    public async Task<IActionResult> UpdateRequest(int id, [FromBody] Request request)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = EnablingApiUrl + "/" + id;
            var json = JsonSerializer.Serialize<Request>(request);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
                return NoContent();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else
                return BadRequest();
        }
    }
}