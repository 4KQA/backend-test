using Microsoft.AspNetCore.Mvc;
using SurvivorAPI.Services;
using SurvivorAPI.Models;

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

    private readonly IPersonRepository _personRepository;

    public SurvivorController(ILogger<SurvivorController> logger, IPersonRepository personRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
    }

    [HttpGet]
    public async Task<List<PersonDTO>> GetPersons() => await _personRepository.ReadPersons();

    [HttpPost]
    public async Task<PersonDTO> PostPerson(string firstName, string lastName, int age, string gender, double lastLatitude, double lastLongitude, int status) 
    => await _personRepository.CreatePerson(new PersonDTO(firstName, lastName, age, gender, lastLatitude, lastLongitude, status));

}
