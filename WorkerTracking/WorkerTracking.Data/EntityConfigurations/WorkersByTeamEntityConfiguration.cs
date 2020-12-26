using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            builder.HasOne(x => x.Team)
                .WithMany()
                .HasForeignKey(x => x.TeamId);

            builder.HasOne(x => x.Worker)
                .WithMany()
                .HasForeignKey(x => x.WorkerId);

        }
    }
}
