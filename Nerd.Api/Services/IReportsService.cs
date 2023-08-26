using Nerd.Api.Contracts;
using Nerd.Api.Models;

namespace Nerd.Api.Services
{
    public interface IReportsService
    {
        void CreatePerson(Person person);
        Person? GetPerson(Guid id);
        GeoLocation UpdateLocation(Guid id, GeoLocation location);
        int GetPercentageSurvivors();
        List<Person> GetSurvivors();
        List<Person> FindByLastName(string lastName);
    }
}