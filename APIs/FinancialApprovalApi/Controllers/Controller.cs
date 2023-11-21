using System.Text;
using FinancialApprovalApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApprovalApi.Controllers;

[ApiController]
public class Controller : ControllerBase
{

    private readonly string EnablingApiUrl = "http://localhost:3005/requests";

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
    public async Task<IActionResult> UpdateRequest(int id)
    {
        using (HttpClient client = new HttpClient())
        {

            HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(EnablingApiUrl, content);
            if (response.IsSuccessStatusCode)
                return NoContent();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();
            else
                return BadRequest();
        }
    }
}