using BackendTest_AKQA.Models;
using BackendTest_AKQA.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace BackendTest_AKQA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepo repo;

        public PeopleController(IPersonRepo personRepo)
        {
            repo = personRepo;
        }

        [HttpGet("all")]
        public IEnumerable<Person> GetPeople()
        {
            return repo.GetPeople();
        }

        [HttpGet("search/{lastName}")]
        public IEnumerable<Person> SearchByLName(string lastName)
        {
            return repo.SearchByLastName(lastName);
        }

        [HttpGet("{id}")]
        public Person GetPerson(int id)
        {
            return GetPerson(id);
        }

        [HttpGet("stats")]
        public Statistics GetStatistics()
        {
            return repo.GetStatistics();
        }

        [HttpPost]
        public void CreatePerson([FromBody] Person person)
        {
            repo.CreatePerson(person);
        }

        [HttpPut("{id}")]
        public string UpdatePerson(int id, [FromBody] Person person)
        {
            return repo.UpdatePerson(id, person);
        }

        [HttpDelete("{id}")]
        public void DeletePerson(int id)
        {
            repo.DeletePerson(id);
        }
    }
}
