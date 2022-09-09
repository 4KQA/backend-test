using AKQA_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AKQA_Backend.Helpers
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("connection"));
        }

        public DbSet<People> People { get; set; }
    }
}
