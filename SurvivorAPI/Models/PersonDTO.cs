namespace SurvivorAPI.Models
{
    public class PersonDTO
    {

        public PersonDTO()
        {

        }


        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public double LastLatitude { get; set; }
        public double LastLongitude { get; set; }
        public int Status { get; set; }

    }
}