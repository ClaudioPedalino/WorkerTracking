using Microsoft.EntityFrameworkCore;
using System;
using WorkerTracking.Data.EntityConfigurations;
using WorkerTracking.Entities;

namespace WorkerTracking.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }


        public DbSet<Worker> Workers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<WorkersByTeam> WorkersByTeams { get; set; }
        public DbSet<Status> Status { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkersEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RolesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TeamsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StatusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new WorkersByTeamEntityConfiguration());

            modelBuilder.Entity<Status>().HasData(new Status[]
            { new Status (1, "Pepe") ,
              new Status (2, "Tete") }
            );
        }
    }
}
