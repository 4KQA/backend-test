using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SurvivorAPI.Models
{
    public class PersonContext : DbContext
    {
        public DbSet<PersonDTO> Tasks { get; set; }
        public string DbPath { get; }

        public PersonContext()
        {
            DbPath = "bin/PersonDTO.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonDTO>().ToTable("Persons");
        }
    }
}