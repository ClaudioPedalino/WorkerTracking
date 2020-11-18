using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerTracking.Entities;

namespace WorkerTracking.Data.EntityConfigurations
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");

            builder.Property(e => e.LocationId)
                .HasAnnotation("Relational:ColumnName", "LocationId")
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.LocationId);

            builder.Property(x => x.LocationName)
                .HasMaxLength(50)
                .HasAnnotation("Relational:ColumnName", "LocationName");
        }
    }
}
