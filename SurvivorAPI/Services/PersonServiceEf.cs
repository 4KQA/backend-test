
using Microsoft.EntityFrameworkCore;
using SurvivorAPI.Controllers;
using SurvivorAPI.Models;



namespace SurvivorAPI.Services
{
    public class PersonServiceEF : IPersonRepository
    {
        private readonly string? _dbConnection = Environment.GetEnvironmentVariable("connectionstring");
        private readonly ILogger<SurvivorController> _logger;

        public PersonServiceEF(ILogger<SurvivorController> logger)
        {
            _logger = logger;
        }

        public async Task<List<PersonDTO>> ReadPersons()
        {

            using var db = new PersonContext();
            Console.WriteLine($"Database path: {db.DbPath}.");

            // Read
            Console.WriteLine("Read all tasks");
            return await db.PersonDTOs.ToListAsync();

        }

        public async Task<List<PersonDTO>> ReadPersonsLastName(string lastName)
        {

            throw new NotImplementedException();

        }

        //Post Person
        public async Task<PersonDTO> CreatePerson(PersonDTO PersonDTO)
        {

            using (var db = new PersonContext())
            {
                Console.WriteLine($"Database path: {db.DbPath}.");

                // Create
                Console.WriteLine("Add person");
                await db.AddAsync(PersonDTO);
                await db.SaveChangesAsync();
                Console.WriteLine(PersonDTO.Id);
                return PersonDTO;

            }

        }


        public async Task<PersonDTO> UpdatePerson(PersonDTO PersonDTO)
        {

            throw new NotImplementedException();

        }
    }
}

