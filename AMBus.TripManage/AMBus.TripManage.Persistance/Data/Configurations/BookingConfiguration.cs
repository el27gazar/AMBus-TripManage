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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.TotalPrice)
             .HasColumnType("decimal(10,2)")
             .IsRequired();

            b.Property(x => x.Status)
             .HasConversion<string>()
             .HasMaxLength(20);

            b.Property(x => x.QrCode)
             .HasMaxLength(20);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            b.HasOne(x => x.User)
             .WithMany(u => u.Bookings)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Trip)
             .WithMany(t => t.Bookings)
             .HasForeignKey(x => x.TripId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Payment)
             .WithOne(p => p.Booking)
             .HasForeignKey<Payment>(p => p.BookingId)
             .OnDelete(DeleteBehavior.Restrict);
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }

}
