namespace Nerd.Api.Models;
public class Person
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public int Age { get; }
    public string Gender { get; }
    public bool IsAlive { get; }
    public GeoLocation Location { get; set; }

    public Person(Guid id, string firstName, string lastName, string gender, int age, bool isAlive, GeoLocation location)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Age = age;
        IsAlive = isAlive;
        Location = location;
    }
}