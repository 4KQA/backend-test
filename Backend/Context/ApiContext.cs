using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {
            if(Survivors.Count() <= 0){
                fillerdata(10);
            }
        }
        public DbSet<Survivor> Survivors { get; set; }

        public void fillerdata(int ammount){
            Random rand = new Random();
            string [] genders = {"Male", "Female"};
            for(int i = 1; i < 20; i++){
                Survivors.Add(new Survivor{
                    Survivor_ID = i,
                    firstName = "tester" + i,
                    lastName = "lastname" + rand.Next(1,5),
                    age = rand.Next(20,80),
                    gender = genders[rand.Next(genders.Count())],
                    longitude = GetRandomNumber(-180,180),
                    latitude = GetRandomNumber(-90,90),
                    alive = i % 2 == 0 ? true : false
                });
            }
            SaveChanges();
        }
        public double GetRandomNumber(double minimum, double maximum)
        { 
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
    }

}