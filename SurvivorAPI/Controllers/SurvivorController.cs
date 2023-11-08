using Microsoft.AspNetCore.Mvc;
using SurvivorAPI.Services;
using SurvivorAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace SurvivorAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SurvivorController : ControllerBase
{


    private readonly ILogger<SurvivorController> _logger;

    private readonly IPersonRepository _personRepository;

    public SurvivorController(ILogger<SurvivorController> logger, IPersonRepository personRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
    }

    [EnableCors]
    [HttpGet("Persons")]
    public async Task<List<PersonDTO>> GetPersons() => await _personRepository.ReadPersons();

    [EnableCors]
    [HttpGet("lastname")]
    public async Task<List<PersonDTO>> GetPersonsLastName(string lastName)
        => await _personRepository.ReadPersonsLastName(lastName);

    [EnableCors]
    [HttpGet("survival")]
    public async Task<double> GetSurvivalRate()
        => await _personRepository.ReadSurvivalRate();

    [EnableCors]
    [HttpPost]
    public async Task<PersonDTO> PostPerson(PersonDTO person)
        => await _personRepository.CreatePerson(person);

    [EnableCors]
    [HttpPut]
    public async Task<PersonDTO> UpdatePerson(PersonDTO person)
        => await _personRepository.UpdatePerson(person);

}
