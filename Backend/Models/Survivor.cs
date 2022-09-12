using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Survivor{
        [Key]
        public int Survivor_ID {get; set;}
        public string firstName {get; set;}
        public string lastName {get; set;}
        public int age {get; set;}
        public string gender {get; set;}
        public double longitude {get; set;}
        public double latitude {get; set;}
        public bool alive {get; set;}
    }
}