namespace SurvivorApp.Models

{
    public class Person
    {

    public Person()
        {

        }

        public Person(string firstName, string lastName, int age, string gender, double lastLatitude, double lastLongitude, bool alive)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
            LastLatitude = lastLatitude;
            LastLongitude = lastLongitude;
            Alive = alive;
        }


        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public double? LastLatitude { get; set; }
        public double? LastLongitude { get; set; }
        public bool? Alive { get; set; }

    }
}