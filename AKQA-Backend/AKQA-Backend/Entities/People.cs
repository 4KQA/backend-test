using System.ComponentModel.DataAnnotations;

namespace AKQA_Backend.Entities
{
    public class People
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string Flag { get; set; }
    }
}
