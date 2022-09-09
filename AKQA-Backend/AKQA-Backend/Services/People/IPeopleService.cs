using AKQA_Backend.Entities;
using AKQA_Backend.Models;

namespace AKQA_Backend.Services.PeopleService
{
    public interface IPeopleService
    {
        void CreatePerson(CreatePeople model);
        void UpdatePerson(int id, UpdatePeople model);
        IEnumerable<People> GetAllPeople();
        People GetPersonByLastName(string lastname);
        People GetPercentage(People people);
    }
}
