using Microsoft.EntityFrameworkCore;
using NERD.Models;

namespace NERD.Database;

public class NerdContext : DbContext
{
    public NerdContext(DbContextOptions<NerdContext> options)
        : base(options)
    {
    }
    
    public DbSet<SurvivorModel> Survivors { get; set; }
}