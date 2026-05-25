using AMBus.TripManage.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Data.Configurations
{

    public class BusConfiguration : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> b)
        {
            b.HasKey(x => x.Id);

            b.HasIndex(x => x.PlateNumber).IsUnique();

            b.Property(x => x.PlateNumber)
             .HasMaxLength(20).IsRequired();

            b.Property(x => x.Model)
             .HasMaxLength(100).IsRequired();

            b.Property(x => x.Type)
             .HasConversion<string>()
             .HasMaxLength(20);

            b.Property(x => x.IsActive)
             .HasDefaultValue(true);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            b.Property(t => t.CreatedBy)
             .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);

            // --- Seed Data ---
            b.HasData(
                new Bus
                {
                    
                    Id = Guid.Parse("a2e34b73-475b-ab84-1236-b77bc807d26f"),
                    PlateNumber = "أ ب ج 1234",
                    Model = "Mercedes-Benz Travego",
                    TotalSeats = 50, 
                    Type = BusType.Standard,
                    IsActive = true
                },
                new Bus
                {
                    Id = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000001"),
                    PlateNumber = "ق ر س 5678",
                    Model = "Volvo 9700",
                    TotalSeats = 45,
                    Type = BusType.Standard,
                    IsActive = true
                },
                new Bus
                {
                    Id = Guid.Parse("bbbbbbbb-0000-0000-0000-000000000002"),
                    PlateNumber = "ط ي ع 9101",
                    Model = "Scania Touring",
                    TotalSeats = 48,
                    Type = BusType.VIP,
                    IsActive = true
                }
            );
        }
    }

}
