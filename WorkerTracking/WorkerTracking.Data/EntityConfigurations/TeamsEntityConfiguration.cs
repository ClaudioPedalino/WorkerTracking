using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.EntityConfigurations
{
    public class TeamsEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.Property(e => e.TeamId)
                .HasAnnotation("Relational:ColumnName", "TeamId")
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.TeamId);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .HasAnnotation("Relational:ColumnName", "Name");
        }
    }
}
