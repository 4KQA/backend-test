using Microsoft.EntityFrameworkCore;

namespace BackendTest_AKQA.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
            if (People.Count() < 1)
            {
                GeneratePeople();
            }
        }

        public DbSet<Person> People { get; set; }

        Random rnd = new Random();

        //Generates dummy data for the People database
        public void GeneratePeople()
        {
            string[] genders = { "Female", "Male", "Other" };
            string[] firstNames = { "Sam", "Alex", "Kim", "Charlie", "Niki", "Pil", "Mark", "Bo", "Cecilie" };
            string[] lastNames = { "Hansen", "Petersen", "Karlsen", "Larsen", "Eriksen", "Pedersen", "Andersen" };
            for (int i = 0; i < 15; i++)
            {
                People.Add(new Person
                {
                    FirstName = firstNames[rnd.Next(firstNames.Length)],
                    LastName = lastNames[rnd.Next(lastNames.Length)],
                    Age = rnd.Next(0, 100),
                    Gender = genders[rnd.Next(genders.Length)],
                    Longitude = GenerateNumber(-180, 180),
                    Latitude = GenerateNumber(-90, 90),
                    // 0 = false, 1 = true
                    Alive = rnd.Next(2) == 1
                });
            }
            SaveChanges();
        }

        private double GenerateNumber(double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }
    }
}
