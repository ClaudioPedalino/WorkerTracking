using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.EntityConfigurations
{
    public class RolesEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Property(e => e.RoleId)
                .HasAnnotation("Relational:ColumnName", "RoleId")
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.RoleId);

            builder.Property(x => x.Name)
                .HasMaxLength(40)
                .HasAnnotation("Relational:ColumnName", "Name");

            builder.Property(x => x.Abbreviation)
                .HasMaxLength(2)
                .HasAnnotation("Relational:ColumnName", "Abbreviation");
        }
    }
}
