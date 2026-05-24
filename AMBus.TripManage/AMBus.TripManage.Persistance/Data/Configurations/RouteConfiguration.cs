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

            b.Property(x => x.Name)
             .HasMaxLength(100)
             .IsRequired();

            b.Property(x => x.IsActive)
             .HasDefaultValue(true);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");

            b.Property(t => t.CreatedBy)
             .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);

            // Seed Data
            b.HasData(
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"), Name = "Cairo", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002"), Name = "Alexandria", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003"), Name = "Aswan", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004"), Name = "Minya", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005"), Name = "Luxor", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000006"), Name = "Sohag", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000007"), Name = "Asyut", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000008"), Name = "Hurghada", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000009"), Name = "Port Said", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000010"), Name = "Ismailia", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000011"), Name = "Giza", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000012"), Name = "Sharm El-Sheikh", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000013"), Name = "Suez", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000014"), Name = "Mansoura", IsActive = true },
                new Route { Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000015"), Name = "Tanta", IsActive = true }
            );
        }
    }
}