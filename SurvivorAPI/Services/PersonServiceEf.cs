
using Microsoft.EntityFrameworkCore;
using SurvivorAPI.Controllers;
using SurvivorAPI.Models;


namespace SurvivorAPI.Services
{
    public class PersonServiceEF : IPersonRepository
    {
        private readonly ILogger<SurvivorController> _logger;

        public PersonServiceEF(ILogger<SurvivorController> logger)
        {
            _logger = logger;
        }


        public async Task<List<PersonDTO>> ReadPersons()
        {
            using var db = new PersonContext();

            List<PersonDTO> personDTOs = await db.PersonDTOs.ToListAsync();
            if (personDTOs.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {

                    PersonDTO tmpPerson = new("SeedFirstName" + i, "SeedLastName" + i, 20 + i, "SeedGender" + i, 21.12345 - 10 * i, 30.11111 - 11.1 * i, i % 2 == 0);
                    await CreatePerson(tmpPerson);
                }
                await db.SaveChangesAsync();
                personDTOs = await db.PersonDTOs.ToListAsync();
            }
            return personDTOs;

        }

        public async Task<List<PersonDTO>> ReadPersonsLastName(string lastName)
        {

            using var db = new PersonContext();
            return await db.PersonDTOs.Where(p => p.LastName.ToLower().Contains(lastName.ToLower())).ToListAsync();

        }


        public async Task<double> ReadSurvivalRate()
        {

            using var db = new PersonContext();
            List<PersonDTO> PersonDTOs = await ReadPersons();
            double alivePersonsCount = PersonDTOs.Where(p => p.Alive).Count();
            double personsCount = PersonDTOs.Count;

            Console.WriteLine("Alive: " + alivePersonsCount);
            Console.WriteLine("Persons: " + personsCount);

            double survivalRate = alivePersonsCount / personsCount * 100;
            return survivalRate;

        }


        public async Task<PersonDTO> CreatePerson(PersonDTO PersonDTO)
        {

            using var db = new PersonContext();


            await db.AddAsync(PersonDTO);
            await db.SaveChangesAsync();
            return PersonDTO;

        }


        public async Task<PersonDTO> UpdatePerson(PersonDTO person)
        {
            using var db = new PersonContext();

            PersonDTO tmpPersonDTO = await db.PersonDTOs.FirstAsync(p => p.Id == person.Id);

            if (Math.Sign(person.LastLatitude) != Math.Sign(tmpPersonDTO.LastLatitude))
            {
                throw new InvalidDataException("You cannot move from one hemisphere to another, they've become semispheres!");
            }
            else if (person.LastLatitude == 0)
            {

                throw new InvalidDataException("Equator quite literally does not exist, you cannot be here!");

            }
            else
            {
                tmpPersonDTO.LastLatitude = Math.Round(person.LastLatitude, 5);
                tmpPersonDTO.LastLongitude = Math.Round(person.LastLongitude, 5);
                tmpPersonDTO.Alive = person.Alive;
            }

            await db.SaveChangesAsync();
            return tmpPersonDTO;

        }
    }
}

