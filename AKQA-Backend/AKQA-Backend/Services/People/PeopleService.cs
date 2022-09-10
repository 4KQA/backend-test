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
            //check if the person the is being created is within the parameters
            var person = mapper.Map<People>(model);

            bool CheckLatPlus = model.Latitude > 0;
            bool CheckLonPlus = model.Longitude > 90;
            bool checkLonNorth = model.Longitude < -90;

            bool CheckLatMinus = model.Latitude < 0;
            bool CheckLonMinus = model.Longitude < 90;
            bool checkLonSouth = model.Longitude > -90;

            //if person is to the north part
            if (CheckLatPlus == true && CheckLonPlus == true || CheckLatPlus == true && checkLonNorth == true)
            { 
                context.People.Add(person);
                context.SaveChanges();
            }
            //if personn is to the south part
            else if (CheckLatMinus == true && CheckLonMinus == true || CheckLatMinus == true && checkLonSouth == true)
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            //if there has been a mix up og lat and long cods
            else 
                throw new AppException("sorry thats not on the same side ps remember the gap");
        }

        public void UpdatePerson(int id, UpdatePeople model)
        {
            //check if the person the is being updated is within the parameters
            var People = getPersonById(id);
            bool CheckLatPlus = People.Latitude > 0;
            bool CheckLonPlus = People.Longitude > 90;
            bool checkLonNorth = People.Longitude < -90;

            bool CheckLatMinus = People.Latitude < 0;
            bool CheckLonMinus = People.Longitude < 90;
            bool checkLonSouth = People.Longitude > -90;

            //if person is to the north part
            if (CheckLatPlus == true && CheckLonPlus == true || CheckLatPlus == true && checkLonNorth == true)
                //check if the person "moved" to the south side
                if (model.Latitude > 0 && model.Longitude > 90 || model.Latitude > 0 && model.Longitude < -90)
                {
                    //if flag or age has not change keep old value
                    if (!string.IsNullOrEmpty(model.Flag))
                        People.Flag = model.Flag;
                    if (model.Age != null)
                        People.Age = model.Age;
                    mapper.Map(model, People);
                    //push updates to person
                    context.People.Update(People);
                    context.SaveChanges();
                }
                //throw error if person tries to move sides
                else
                    throw new AppException("You cant cross, there is a big gap in between");

            //if personn is to the south part
            else if (CheckLatMinus == true && CheckLonMinus == true || checkLonSouth == true)
                //check if the person "moved" to the north side
                if (model.Latitude < 0 && model.Longitude < 90 || model.Latitude < 0 && model.Longitude > -90)
                {
                    //if flag or age has not change keep old value
                    if(!string.IsNullOrEmpty(model.Flag))
                        People.Flag = model.Flag;
                    if(model.Age != null)
                        People.Age = model.Age;
                    mapper.Map(model, People);
                    //push updates to person
                    context.People.Update(People);
                    context.SaveChanges();
                }
                //throw error if person tries to move sides
                else
                    throw new AppException("You cant cross, there is a big gap in between");
            //give error if the lat and long cods diden make sense
            else
                throw new AppException("sorry the data given diden't make sense");

        }
        //get person by id
        private People getPersonById(int id)
        {
            var person = context.People.Find(id);
            //if person does not exists throw error
            if (person == null)
                throw new KeyNotFoundException("Person not found");
            return person;
        }

        //get every person
        public IEnumerable<People> GetAllPeople()
        {
            return context.People;
        }
        //get person by lastname
        public IEnumerable<People> GetPersonByLastName(string lastname)
        {
            var results = GetAllPeople();
            return results.Where(x => x.LastName.ToLower() == lastname.ToLower());
        }

        //get percentage of people alive
        public string GetPercentage()
        {
            //get every person that has a flag named dead or alive
            var resultDead = context.People.Where(X => X.Flag.Contains("dead"));
            var resultAlive = context.People.Where(X => X.Flag.Contains("alive"));
            //added every person up for percentage calculation
            double totalPeople = resultDead.ToList().Count + resultAlive.ToList().Count;

            //total percentage of people is alive
            double totalPercentage = resultAlive.ToList().Count / totalPeople *100;
            
            return resultAlive.ToList().Count + " is alive today out of: " + totalPeople + " which is " + totalPercentage + " %";
        }
    }
}
