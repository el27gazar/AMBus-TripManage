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
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.SeatNumber)
             .HasMaxLength(10).IsRequired();

            b.Property(x => x.IsAvailable)
             .HasDefaultValue(true);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            b.HasOne(s => s.Bus)
             .WithMany(bus => bus.Seats)
             .HasForeignKey(s => s.BusId)
             .OnDelete(DeleteBehavior.Cascade);
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }
}
