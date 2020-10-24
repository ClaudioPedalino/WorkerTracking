using Microsoft.EntityFrameworkCore;
using System;
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

		}
	}
}
