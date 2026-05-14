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
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.FromCity)
             .HasMaxLength(100).IsRequired();

            b.Property(x => x.ToCity)
             .HasMaxLength(100).IsRequired();

            b.Property(x => x.IsActive)
             .HasDefaultValue(true);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }
}
