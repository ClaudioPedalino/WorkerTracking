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

            builder.HasAnnotation("Relational:TableName", "Teams");

            builder.HasKey(e => e.TeamId);

            builder.Property(e => e.TeamId)
                .HasAnnotation("Relational:ColumnName", "TeamId")
                .ValueGeneratedOnAdd();


            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasAnnotation("Relational:ColumnName", "Name");
        }
    }
}
