using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NERD.Models;

[Table("NERD")]
public class SurvivorModel
{
    [Key] public int id { get; set; }

    [Column("first_name")]
    public string firstName { get; set; }
    
    [Column("last_name")]
    public string lastName { get; set; }
    public string gender { get; set; }
    public double longitude { get; set; }
    public double latitude { get; set; }
    public string isAlive { get; set; }

    public void SetProperty(string itemName, string toString)
    {
        // set property itemName to toString
        switch (itemName)
        {
            case "id":
                id = int.Parse(toString);
                break;
            case "firstName":
                firstName = toString;
                break;
            case "lastName":
                lastName = toString;
                break;
            case "gender":
                gender = toString;
                break;
            case "latitude":
                latitude = float.Parse(toString);
                break;
            case "longitude":
                longitude = float.Parse(toString);
                break;
            case "isAlive":
                isAlive = toString;
                break;
            default:
                Console.WriteLine($"Something went wrong with {itemName}");
                break;
        }
    }
}