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
            // بتتعمل أوتوماتيك من ApplyAudit في SaveChanges
            b.Property(t => t.CreatedBy)
             .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);

            // ── Relations ────────────────────────────────
            b.HasOne(t => t.Route)
             .WithMany(r => r.Trips)
             .HasForeignKey(t => t.RouteId)
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
