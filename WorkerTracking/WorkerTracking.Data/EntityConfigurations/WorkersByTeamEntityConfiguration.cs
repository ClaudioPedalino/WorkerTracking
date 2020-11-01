using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.EntityConfigurations
{
    public class WorkersByTeamEntityConfiguration : IEntityTypeConfiguration<WorkersByTeam>
    {
        public void Configure(EntityTypeBuilder<WorkersByTeam> builder)
        {
            builder.ToTable("WorkersByTeams");

            builder.Property(e => e.WorkersByTeamId)
                .HasAnnotation("Relational:ColumnName", "WorkersByTeamId")
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.WorkersByTeamId);
            
            #region relations

            builder.HasMany<Team>()
                .WithOne()
                .HasForeignKey(x => x.TeamId);

            builder.HasMany<Worker>()
                .WithOne()
                .HasForeignKey(x => x.WorkerId);

            #endregion
        }
    }
}
