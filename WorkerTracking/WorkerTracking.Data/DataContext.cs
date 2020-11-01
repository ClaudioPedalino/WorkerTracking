using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Linq;
using WorkerTracking.Data.EntityConfigurations;
using WorkerTracking.Entities;

namespace WorkerTracking.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}


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
            
            SeedInitialData(modelBuilder);
            
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Status>().HasData(new Status[]
            {
                new Status (101, "Active"),
                new Status (102, "Inactive"),
                new Status (103, "Pause"),
                new Status (104, "Vacations"),
                new Status (105, "In a meeting")
            });
            
            //modelBuilder.HasSequence<Status>("status_seq", schema : "public")
            //    .StartsAt(6);

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role (5001, "Product Owner", "PO"),
                new Role (5002, "Project Manager", "PM"),
                new Role (5003, "Team Leader", "TL"),
                new Role (5004, "Frontend Developer", "FD"),
                new Role (5005, "Backeck Developer", "BD"),
                new Role (5006, "Fullstack Developer", "FS"),
                new Role (5007, "Quality Assurance", "QA"),
                new Role (5008, "User Experience", "UX"),
                new Role (5009, "Functional Analyst", "FA"),
                new Role (5010, "Graphic Designer", "GD"),
                new Role (5011, "Human Resources", "HR"),
                new Role (5012, "Technical Support", "TS"),
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
