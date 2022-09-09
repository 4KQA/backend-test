using AKQA_Backend.Entities;
using AKQA_Backend.Models;
using AKQA_Backend.Services.PeopleService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AKQA_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService service;
        private IMapper mapper;

        public PeopleController(IPeopleService peopleService, IMapper mapper)
        {
            this.service = peopleService;
            this.mapper = mapper;
        }
        [HttpPost("Add person")]
        public IActionResult CreatePerson(CreatePeople model)
        {
            service.CreatePerson(model);
            return Ok(new { message = "Person registered" });
        }

        [HttpGet("Get all people")]
        public IActionResult GetallPeople()
        {
            var people = service.GetAllPeople();
            return Ok(people);
        }

        [HttpGet("Get person by LastName")]
        public IActionResult GetPersonByLastName(string lastname)
        {
            var person = service.GetPersonByLastName(lastname);
            return Ok(person);
        }

        [HttpPut("Update your location/{id}")]
        public IActionResult UpdatePerson(int id,UpdatePeople model)
        {
            service.UpdatePerson(id, model);
            return Ok(new {message = "Person has been updated"});
        }

        [HttpGet("Get procentage of survivors")]
        public IActionResult GetPercentage()
        {
            var Procentage = service.GetPercentage();
            return Ok(Procentage);
        }

    }
}
