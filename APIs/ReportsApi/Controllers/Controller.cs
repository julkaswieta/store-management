using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace ReportsApi.Controllers;

[ApiController]
[Route("reports")]
public class Controller : ControllerBase
{
    private string? reportToSend;

    public Controller() { }

    [HttpGet]
    [Route("today")]
    public IActionResult GetTodaysReport()
    {
        LoadFile("./Data/daily.json");
        return Ok(reportToSend);
    }

    [HttpGet]
    [Route("weekly")]
    public IActionResult GetWeeklyReport()
    {
        LoadFile("./Data/weekly.json");
        return Ok(reportToSend);
    }

    [HttpGet]
    [Route("monthly")]
    public IActionResult GetMonthlyReport()
    {
        LoadFile("./Data/monthly.json");
        return Ok(reportToSend);
    }

    [HttpGet]
    [Route("yearly")]
    public IActionResult GetYearlyReport()
    {
        LoadFile("./Data/yearly.json");
        return Ok(reportToSend);
    }

    private void LoadFile(string filename)
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            reportToSend = reader.ReadToEnd();
        }
    }
}