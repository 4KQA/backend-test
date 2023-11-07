using System;
using System.Configuration;
using SurvivorAPI.Services;

namespace SurvivorAPI.Models
{
    public class DbConnection
    {
        public DbConnection(WebApplicationBuilder builder){

            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection String has not been initialized");
            }
            else if (connectionString.ToLower().Contains("entity-framework"))
            {
                builder.Services.AddScoped<IPersonRepository, PersonServiceEF>();
                
            }
            else throw new InvalidOperationException("Connection String does not contain a database specification");
        }
        public string? connectionString = Environment.GetEnvironmentVariable("connectionstring");

    }
}
