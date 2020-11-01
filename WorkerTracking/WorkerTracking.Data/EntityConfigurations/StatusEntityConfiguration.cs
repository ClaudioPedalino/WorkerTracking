using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.EntityConfigurations
{
    public class StatusEntityConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");

            builder.Property(e => e.StatusId)
                .HasAnnotation("Relational:ColumnName", "StatusId")
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.StatusId);

            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .HasAnnotation("Relational:ColumnName", "Name");

        }
    }
}
