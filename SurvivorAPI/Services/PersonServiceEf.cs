
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
        public async Task<int> CreatePerson(PersonDTO PersonDTO)
        {

            using (var db = new PersonContext())
            {
                Console.WriteLine($"Database path: {db.DbPath}.");

                PersonDTO tmpPerson = new();
                // Create
                Console.WriteLine("Add person");
                await db.AddAsync(tmpPerson);
                await db.SaveChangesAsync();
                Console.WriteLine(tmpPerson.Id);
                return (int)tmpPerson.Id;

            }

        }


        public async Task<PersonDTO> UpdatePerson(PersonDTO PersonDTO)
        {

            throw new NotImplementedException();

        }
    }
}

