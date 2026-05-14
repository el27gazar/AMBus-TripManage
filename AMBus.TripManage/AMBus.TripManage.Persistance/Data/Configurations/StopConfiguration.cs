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
    public class StopConfiguration : IEntityTypeConfiguration<Stop>
    {
        public void Configure(EntityTypeBuilder<Stop> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.CityName)
             .HasMaxLength(100).IsRequired();

            b.Property(x => x.StationAddress)
             .HasMaxLength(200);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            b.HasOne(s => s.Route)
             .WithMany(r => r.Stops)
             .HasForeignKey(s => s.RouteId)
             .OnDelete(DeleteBehavior.Cascade);

            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }
}
