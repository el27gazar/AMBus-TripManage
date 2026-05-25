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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> b)
        {
            b.HasKey(x => x.Id);

            b.HasIndex(x => x.LicenseNumber).IsUnique();

            b.Property(x => x.LicenseNumber)
             .HasMaxLength(50).IsRequired();

            b.Property(x => x.EmergencyContact)
             .HasMaxLength(20);

            b.Property(x => x.IsAvailable)
             .HasDefaultValue(true);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            // one-to-one Driver ↔ User
            b.HasOne(d => d.User)
             .WithOne(u => u.Driver)
             .HasForeignKey<Driver>(d => d.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            b.Property(t => t.CreatedBy)
             .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);

          
        }
    }
}
