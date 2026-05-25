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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.Property(u => u.FullName)
             .HasMaxLength(100)
             .IsRequired();

            b.Property(u => u.IsActive)
             .HasDefaultValue(true);

            b.Property(u => u.CreatedAt)
             .HasDefaultValueSql("GETUTCDATE()");

            // --- Seed Data (للمستخدمين السائقين) ---
            b.HasData(
                new User
                {
                    Id = Guid.Parse("cccccccc-0000-0000-0000-000000000001"),
                    FullName = "كابتن محمد أحمد",
                    Email = "driver1@ambus.com",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("cccccccc-0000-0000-0000-000000000002"),
                    FullName = "كابتن محمود علي",
                    Email = "driver2@ambus.com",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
    }

