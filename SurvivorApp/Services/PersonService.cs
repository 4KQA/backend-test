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
    private readonly string? baseAPI = "";



    public PersonService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        baseAPI = configuration["base_api"];
    }


    public async Task<List<Person>?> GetPersons()
    {
        try
        {
            string url = $"{baseAPI}persons/";
            return await http.GetFromJsonAsync<List<Person>>(url);
        }
        catch
        {
            throw new InvalidOperationException();
        }

    }

    public async Task<List<Person>?> GetPersonsLastName(string lastName)
    {

        try
        {
            string url = $"{baseAPI}lastname?lastName=" + lastName;
            return await http.GetFromJsonAsync<List<Person>>(url);
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }



    public async Task<double> GetSurvivalRate()
    {
        try
        {

            string url = $"{baseAPI}survival";
            return await http.GetFromJsonAsync<double>(url);
        }
        catch
        {
            throw new InvalidOperationException();
        }

    }


    public async Task<Person> UpdatePerson(Person person)
    {
        string url = $"{baseAPI}";

        try
        {
            await http.PutAsJsonAsync<Person>(url, person);
            return person;
        }
        catch
        {
            throw new InvalidOperationException();
        }



    }

    public async Task<Person> CreatePerson(Person person)
    {
        string url = $"{baseAPI}";
        Console.WriteLine(person.FirstName + person.LastName + person.Age + person.Gender + person.LastLatitude + person.LastLongitude + person.Alive);
        try
        {
            await http.PostAsJsonAsync<Person>(url, person);
            return person;
        }
        catch
        {
            throw new InvalidOperationException();
        }


    }



}