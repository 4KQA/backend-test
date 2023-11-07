using System;
using SurvivorAPI.Models;

namespace SurvivorAPI.Services
{
    public interface IPersonRepository
    {
        Task <PersonDTO> CreatePerson(PersonDTO PersonDTO);
        Task<List<PersonDTO>> ReadPersons();
        Task<List<PersonDTO>> ReadPersonsLastName(string lastName);
        Task<PersonDTO> UpdatePerson(PersonDTO PersonDTO);

        
    }
}