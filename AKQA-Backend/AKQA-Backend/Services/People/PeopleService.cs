using AKQA_Backend.Entities;
using AKQA_Backend.Helpers;
using AKQA_Backend.Models;
using AutoMapper;

namespace AKQA_Backend.Services.PeopleService
{
    public class PeopleService : IPeopleService
    {
        private readonly DatabaseContext context;
        private readonly IMapper mapper;

        public PeopleService(DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void CreatePerson(CreatePeople model)
        {

        }

        public void UpdatePerson(UpdatePeople model)
        {

        }

        public IEnumerable<People> GetAllPeople()
        {

        }

        public People GetPersonByLastName(string lastname)
        {

        }

        public void GetPercentage()
        {

        }
    }
}
