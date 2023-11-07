using System;
using SurvivorAPI.Models;

namespace SurvivorServiceAPI.Services
{
    public interface IPersonRepository
    {
        Task <int> CreatePerson(PersonDTO PersonDTO);
        Task<PersonDTO> ReadPerson(int PersonId);
        Task<PersonDTO> UpdatePerson(PersonDTO PersonDTO);
        Task<bool> DeletePerson(int PersonId);
    }
}