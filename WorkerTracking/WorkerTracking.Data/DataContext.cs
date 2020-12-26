using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkerTracking.Core.Enums;
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
        public DbSet<IdentityUser> Users { get; set; }


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
            base.OnModelCreating(modelBuilder);
            ModelBuilderExtensions.UseSnakeCaseNames(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(new Status[]
            {
                new Status ((int)StatusEnum.Active, "Active"),
                new Status ((int)StatusEnum.Inactive, "Inactive"),
                new Status ((int)StatusEnum.Pause, "Pause"),
                new Status ((int)StatusEnum.InMeeting, "In a meeting"),
                new Status ((int)StatusEnum.Vacations, "Vacations")
            });

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role ((int)RolesEnum.ProductOwner, "Product Owner", "PO"),
                new Role ((int)RolesEnum.ProjectManager, "Project Manager", "PM"),
                new Role ((int)RolesEnum.TeamLeader, "Team Leader", "TL"),
                new Role ((int)RolesEnum.FrontendDeveloper, "Frontend Developer", "FD"),
                new Role ((int)RolesEnum.BackendDeveloper, "Backend Developer", "BD"),
                new Role ((int)RolesEnum.FullstackDeveloper, "Fullstack Developer", "FS"),
                new Role ((int)RolesEnum.QualityAssurance, "Quality Assurance", "QA"),
                new Role ((int)RolesEnum.UserExperience, "User Experience", "UX"),
                new Role ((int)RolesEnum.Analyst, "Functional Analyst", "FA"),
                new Role ((int)RolesEnum.Designer, "Graphic Designer", "GD"),
                new Role ((int)RolesEnum.HumanResources, "Human Resources", "HR"),
                new Role ((int)RolesEnum.Support, "Technical Support", "TS"),
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
