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

    
}