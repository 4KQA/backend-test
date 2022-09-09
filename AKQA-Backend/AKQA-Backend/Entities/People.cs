using System.ComponentModel.DataAnnotations;

namespace AKQA_Backend.Entities
{
    public class People
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Flag { get; set; }
    }
}
