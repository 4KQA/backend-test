using System.Data.Common;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SurvivorApp.Models;

namespace SurvivorApp.Services;

public class PersonService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";


    public event Action RefreshRequired;

    public PersonService(HttpClient http, IConfiguration configuration) {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public void CallRequestRefresh()
    {
         RefreshRequired?.Invoke();
    }

    public async Task<List<Person>> GetPersons()
    {
        string url = $"{baseAPI}persons/";
        Console.WriteLine("Base API: " + baseAPI);
        Console.WriteLine("Base URL: " + url);
        return await http.GetFromJsonAsync<List<Person>>(url);
    }

    public async Task<List<Person>> GetPersonsLastName(string lastName)
    {
        string url = $"{baseAPI}lastname?lastName="+ lastName;
        Console.WriteLine("Base API: " + baseAPI);
        Console.WriteLine("Base URL: " + url);
        return await http.GetFromJsonAsync<List<Person>>(url);
    }

    public async Task<double> GetSurvivalRate()
    {
        string url = $"{baseAPI}survival";
        Console.WriteLine("Base API: " + baseAPI);
        Console.WriteLine("Base URL: " + url);
        return await http.GetFromJsonAsync<double>(url);
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        string url = $"{baseAPI}";
        person.Id = 0;
        Console.WriteLine("Base API: " + baseAPI);
        Console.WriteLine("Base URL: " + url);
        var response = await http.PutAsJsonAsync<Person>(url, person);
        Console.WriteLine(response);
        return person;
        

    }

    public async Task<Person> CreatePerson(Person person)
    {
        string url = $"{baseAPI}";
        Console.WriteLine("Base API: " + baseAPI);
        Console.WriteLine("Base URL: " + url);
        Console.WriteLine(person.FirstName + person.LastName + person.Age + person.Gender + person.LastLatitude + person.LastLongitude + person.Alive);
        var response = await http.PostAsJsonAsync<Person>(url, person);
        Console.WriteLine(response);
        return person;
        

    }

    
}