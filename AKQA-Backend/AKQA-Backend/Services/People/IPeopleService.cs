using AKQA_Backend.Entities;
using AKQA_Backend.Models;

namespace AKQA_Backend.Services.PeopleService
{
    public interface IPeopleService
    {
        void CreatePerson(CreatePeople model);
        void UpdatePerson(UpdatePeople model);
        IEnumerable<People> GetAllPeople();
        People GetPersonByLastName(string lastname);
    }
}
