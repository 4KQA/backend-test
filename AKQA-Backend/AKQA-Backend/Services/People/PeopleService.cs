using AKQA_Backend.Entities;
using AKQA_Backend.Helpers;
using AKQA_Backend.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            var person = mapper.Map<People>(model);

            bool CheckLatPlus = model.Latitude > 0;
            bool CheckLonPlus = model.Longitude > 90;
            bool checkLonNorth = model.Longitude < -90;

            bool CheckLatMinus = model.Latitude < 0;
            bool CheckLonMinus = model.Longitude < 90;
            bool checkLonSouth = model.Longitude > -90;

            if (CheckLatPlus == true && CheckLonPlus == true || CheckLatPlus == true && checkLonNorth == true)
            { 
                context.People.Add(person);
                context.SaveChanges();
            }
            else if (CheckLatMinus == true && CheckLonMinus == true || CheckLatMinus == true && checkLonSouth == true)
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            else 
                throw new AppException("sorry thats not on the same side ps remember the gap");
        }

        public void UpdatePerson(int id, UpdatePeople model)
        {
            var People = getPersonById(id);
            bool CheckLatPlus = People.Latitude > 0;
            bool CheckLonPlus = People.Longitude > 90;
            bool checkLonNorth = People.Latitude < -90;

            bool CheckLatMinus = People.Latitude < 0;
            bool CheckLonMinus = People.Longitude < 90;
            bool checkLonSouth = People.Latitude > -90;

            if (CheckLatPlus == true && CheckLonPlus == true || CheckLatPlus == true && checkLonNorth == true)
                if (model.Latitude > 0 && model.Longitude > 90 || model.Latitude > 0 && model.Longitude < -90)
                {
                    mapper.Map(model, People); ;
                    context.People.Update(People);
                    context.SaveChanges();
                }
                else
                    throw new AppException("You cant cross, there is a big gap in between");
            else if (CheckLatMinus == true && CheckLonMinus == true || checkLonSouth == true)
                if (model.Latitude < 0 && model.Longitude < 90 || model.Latitude < 0 && model.Longitude > -90)
                {
                    if(!string.IsNullOrEmpty(model.Flag))
                        People.Flag = model.Flag;
                    if(model.Age != null)
                        People.Age = model.Age;
                    mapper.Map(model, People); ;
                    context.People.Update(People);
                    context.SaveChanges();
                }
                else
                    throw new AppException("You cant cross, there is a big gap in between");
            else
                throw new AppException("sorry the data given diden't make sense");

        }

        private People getPersonById(int id)
        {
            var person = context.People.Find(id);
            if (person == null)
                throw new KeyNotFoundException("Person not found");
            return person;
        }

        public IEnumerable<People> GetAllPeople()
        {
            return context.People;
        }

        public People GetPersonByLastName(string lastname)
        {
            return context.People.FirstOrDefault(x => x.LastName.Contains(lastname));
        }

        public string GetPercentage()
        {
            var resultDead = context.People.Where(X => X.Flag.Contains("dead"));
            var resultAlive = context.People.Where(X => X.Flag.Contains("alive"));
            double totalPeople = resultDead.ToList().Count + resultAlive.ToList().Count;
            double totalAlive = resultAlive.ToList().Count;

            double totalPercentage = totalAlive / totalPeople *100;
            
            return resultAlive.ToList().Count + " is alive today out of: " + totalPeople + " which is " + totalPercentage + " %";
        }
    }
}
