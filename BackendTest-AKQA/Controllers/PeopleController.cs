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

        //This returns all people in the database
        [HttpGet("all")]
        public IEnumerable<Person> GetPeople()
        {
            return repo.GetPeople();
            
        }

        //Returns a list of people mathcing the search parameter
        [HttpGet("search")]
        public IEnumerable<Person> SearchByLName(string lastName)
        {
            return repo.SearchByLastName(lastName);
        }

        //Returns the person matching the id
        [HttpGet("{id}")]
        public Person GetPerson(int id)
        {
            return GetPerson(id);
        }

        //Gets statistics over data
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
