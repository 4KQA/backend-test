namespace AKQA_Backend.Models
{
    //mapping for create
    public class CreatePeople
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Flag { get; set; }
    }
}
