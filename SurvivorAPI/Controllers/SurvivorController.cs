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

    [HttpGet("lastname")]
    public async Task<List<PersonDTO>> GetPersonsLastName(string lastName)
        => await _personRepository.ReadPersonsLastName(lastName);

    [HttpGet("survival")]
    public async Task<double> GetSurvivalRate()
        => await _personRepository.ReadSurvivalRate();

    [HttpPost]
    public async Task<PersonDTO> PostPerson(string firstName, string lastName, int age, string gender, double lastLatitude, double lastLongitude, bool alive)
        => await _personRepository.CreatePerson(new PersonDTO(firstName, lastName, age, gender, lastLatitude, lastLongitude, alive));

    [HttpPut]
    public async Task<PersonDTO> UpdatePerson(int id, double lastLatitude, double lastLongitude, bool alive)
        => await _personRepository.UpdatePerson(id, lastLatitude, lastLongitude, alive);

}
