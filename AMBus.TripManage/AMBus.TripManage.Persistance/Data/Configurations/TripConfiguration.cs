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
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> b)
        {
            b.HasKey(t => t.Id);

            b.Property(t => t.BasePrice)
             .HasColumnType("decimal(10,2)")
             .IsRequired();

            b.Property(t => t.Status)
             .HasConversion<string>()
             .HasMaxLength(20);

            // ── Audit fields ──────────────────────────────
            b.Property(t => t.CreatedBy)
             .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);

            // ── Relations ────────────────────────────────
            b.HasOne(t => t.From)
             .WithMany(r => r.DepartureTrips)
             .HasForeignKey(t => t.FromId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(t => t.To)
             .WithMany(r => r.ArrivalTrips)
             .HasForeignKey(t => t.ToId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(t => t.Bus)
             .WithMany(bus => bus.Trips)
             .HasForeignKey(t => t.BusId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(t => t.Driver)
             .WithMany(d => d.Trips)
             .HasForeignKey(t => t.DriverId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
