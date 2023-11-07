
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

            return await db.PersonDTOs.ToListAsync();

        }

        public async Task<List<PersonDTO>> ReadPersonsLastName(string lastName)
        {

            using var db = new PersonContext();
            return await db.PersonDTOs.Where(p => p.LastName.ToLower() == lastName.ToLower()).ToListAsync();

        }

/*
        public async Task<double> ReadSurvivalRate()
        {

            using var db = new PersonContext();
            return await db.PersonDTOs.Where(p => p.LastName.ToLower() == lastName.ToLower()).ToListAsync();

        }
*/

        public async Task<PersonDTO> CreatePerson(PersonDTO PersonDTO)
        {

            using var db = new PersonContext();


            await db.AddAsync(PersonDTO);
            await db.SaveChangesAsync();
            return PersonDTO;

        }


        public async Task<PersonDTO> UpdatePerson(int id, double lastLatitude, double lastLongitude, int status)
        {
            using var db = new PersonContext();
            PersonDTO tmpPersonDTO = await db.PersonDTOs.FirstAsync(p => p.Id == id);

            if (Math.Sign(lastLatitude) != Math.Sign(tmpPersonDTO.LastLatitude)){
                throw new InvalidDataException("You cannot move from one hemisphere to another, they've become semispheres!");
            }
            else if(lastLatitude == 0){

                throw new InvalidDataException("Equator quite literally does not exist, you cannot be here!");
                
            }
            else{
                tmpPersonDTO.LastLatitude = Math.Round(lastLatitude, 5);
                tmpPersonDTO.LastLongitude = lastLongitude;
                tmpPersonDTO.Status = status;
            }
            
            await db.SaveChangesAsync();
            return tmpPersonDTO;

        }
    }
}

