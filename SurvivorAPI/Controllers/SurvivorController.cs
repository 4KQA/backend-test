using Microsoft.AspNetCore.Mvc;

namespace SurvivorAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SurvivorController : ControllerBase
{
    /*
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    */

    private readonly ILogger<SurvivorController> _logger;

    public SurvivorController(ILogger<SurvivorController> logger)
    {
        _logger = logger;
    }

    
}
