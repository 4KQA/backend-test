
using SurvivorAPI.Controllers;
using SurvivorAPI.Models;



namespace SurvivorAPI.Services
{
    public class SqlPersonRepository : IPersonRepository
    {
        private readonly string? _dbConnection = Environment.GetEnvironmentVariable("connectionstring");
        private readonly ILogger<SurvivorController> _logger;

        public SqlPersonRepository(ILogger<SurvivorController> logger)
        {
            _logger = logger;
        }

        public async Task<List<PersonDTO>> ReadPersons()
        {
            
            throw new NotImplementedException();

        }

        public async Task<List<PersonDTO>> ReadPersonsLastName(string lastName)
        {
            
            throw new NotImplementedException();

        }

        //Post Person
        public async Task<int> CreatePerson(PersonDTO PersonDTO)
        {
            
            throw new NotImplementedException();

        }


        public async Task<PersonDTO> UpdatePerson(PersonDTO PersonDTO)
        {
           
           throw new NotImplementedException();

        }
    }
}

