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
        }
        }
    
}
