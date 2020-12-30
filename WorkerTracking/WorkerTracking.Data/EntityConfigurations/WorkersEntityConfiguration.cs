using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.EntityConfigurations
{
    public class WorkersEntityConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Workers");

            builder.Property(e => e.WorkerId)
                .HasAnnotation("Relational:ColumnName", "WorkerId")
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.WorkerId);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("Relational:ColumnName", "FirstName");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("Relational:ColumnName", "LastName");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasAnnotation("Relational:ColumnName", "Email");

            builder.Property(x => x.Birthday)
                .HasDefaultValue(DateTime.MinValue)
                .HasAnnotation("Relational:ColumnName", "Birthday");

            builder.Property(x => x.PhotoUrl)
                .HasAnnotation("Relational:ColumnName", "PhotoUrl");

            builder.Property(x => x.GetStatusId())
                .HasAnnotation("Relational:ColumnName", "StatusId");

            builder.Property(x => x.RoleId)
                .HasAnnotation("Relational:ColumnName", "RoleId");

            builder.Property(x => x.IsActive)
                .HasDefaultValueSql<bool>("true")
                .HasAnnotation("Relational:ColumnName", "IsActive");


            builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.GetStatusId());

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

        }
    }
}
