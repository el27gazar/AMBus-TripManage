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
    public class BookingSeatConfiguration : IEntityTypeConfiguration<BookingSeat>
    {
        public void Configure(EntityTypeBuilder<BookingSeat> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.PassengerName)
             .HasMaxLength(100);

            b.Property(x => x.PassengerIdNumber)
             .HasMaxLength(20);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            b.HasOne(x => x.Booking)
             .WithMany(bk => bk.BookingSeats)
             .HasForeignKey(x => x.BookingId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Seat)
             .WithMany(s => s.BookingSeats)
             .HasForeignKey(x => x.SeatId)
             .OnDelete(DeleteBehavior.Restrict);
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }
}
