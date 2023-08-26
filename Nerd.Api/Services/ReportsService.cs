using Nerd.Api.Contracts;
using Nerd.Api.Models;

namespace Nerd.Api.Services;

public class ReportsService : IReportsService
{
    private static readonly Dictionary<Guid, Person> _people = new();

    public void CreatePerson(Person person)
    {
        _people.Add(person.Id, person);
    }

    public Person? GetPerson(Guid id)
    {
        if (!_people.ContainsKey(id)) return null;
        return _people[id];
    }

    public GeoLocation UpdateLocation(Guid id, GeoLocation location)
    {
        var person = _people[id];
        person.Location = location;
        return location;
    }

    public int GetPercentageSurvivors()
    {
        int total = _people.Count;
        if (total == 0) return 0; // Avoid division by zero

        int survivors = _people.Values.Count(p => p.IsAlive);

        return (int)Math.Round((double)survivors / total * 100);
    }

    public List<Person> GetSurvivors()
    {
        return _people.Values.Where(p => p.IsAlive).ToList();
    }

    public List<Person> FindByLastName(string lastName)
    {
        return _people.Values.Where(p => p.LastName == lastName).ToList();
    }
}