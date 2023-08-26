using Microsoft.AspNetCore.Mvc;
using Nerd.Api.Contracts.Requests;
using Nerd.Api.Contracts.Responses;
using Nerd.Api.Models;
using Nerd.Api.Services;

namespace Nerd.Api;

[Route("[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpPost("create")]
    public IActionResult CreatePerson(CreatePersonRequest request)
    {
        var person = new Person(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Gender,
            request.Age,
            request.IsAlive,
            request.Location
        );

        _reportsService.CreatePerson(person);

        var response = GetPersonResponse(person);

        return Ok(response);
    }

    [HttpPut("update/{id}")]
    public IActionResult UpdateLocation(Guid id, UpdateLocationRequest request)
    {
        var existingPerson = _reportsService.GetPerson(id);
        if (existingPerson is null)
            return BadRequest(new ErrorResponse($"Person with id {id} not found."));

        var location = new GeoLocation(request.Latitude, request.Longitude);

        if (existingPerson.Location.Hemisphere != location.Hemisphere)
            return BadRequest(new ErrorResponse("Cannot change hemisphere."));

        var response = _reportsService.UpdateLocation(id, new GeoLocation(request.Latitude, request.Longitude));

        return Ok(response);
    }

    [HttpGet("percentage")]
    public IActionResult GetPercentageSurvivors()
    {
        var percentage = _reportsService.GetPercentageSurvivors();
        var response = new SurvivorPercentageResponse(percentage);

        return Ok(response);
    }

    [HttpGet("survivors")]
    public IActionResult GetSurvivors()
    {
        List<Person> survivors = _reportsService.GetSurvivors();

        var people = new List<PersonResponse>();
        people.AddRange(survivors.Select(GetPersonResponse));

        var total = _reportsService.GetPercentageSurvivors();

        var response = new SurvivorsResponse(total, survivors);

        return Ok(response);
    }

    [HttpGet("search")]
    public IActionResult FindByLastName(string lastName)
    {
        List<Person> people = _reportsService.FindByLastName(lastName);


        // if (people is null)
        //     return BadRequest(new ErrorResponse($"No people found with last name {lastName}."));

        var response = new List<PersonResponse>();

        // foreach (var person in people)
        // {
        //     response.Add(GetPersonResponse(person));
        // }

        response.AddRange(people.Select(GetPersonResponse));

        return Ok(response);
    }


    private PersonResponse GetPersonResponse(Person person)
    {
        return new PersonResponse(
                    person.Id,
                    person.FirstName,
                    person.LastName,
                    person.Gender,
                    person.Age,
                    person.IsAlive,
                    person.Location
                );
    }
}