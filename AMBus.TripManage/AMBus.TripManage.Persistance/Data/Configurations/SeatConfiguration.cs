using AMBus.TripManage.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

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


            var busesInfo = new[]
            {
                new { Id = "a2e34b73-475b-ab84-1236-b77bc807d26f", TotalSeats = 50 },
                new { Id = "bbbbbbbb-0000-0000-0000-000000000001", TotalSeats = 45 },
                new { Id = "bbbbbbbb-0000-0000-0000-000000000002", TotalSeats = 48 }
            };

            var allSeatsSeed = new List<Seat>();

            foreach (var bus in busesInfo)
            {
                for (int i = 1; i <= bus.TotalSeats; i++)
                {
                    allSeatsSeed.Add(new Seat
                    {
                        Id = CreateDeterministicGuid(bus.Id, i),
                        SeatNumber = i.ToString(),
                        IsAvailable = true,
                        BusId = Guid.Parse(bus.Id)
                    });
                }
            }

            b.HasData(allSeatsSeed);
        }

        private Guid CreateDeterministicGuid(string busId, int seatNumber)
        {
            var input = $"{busId}_{seatNumber}";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                return new Guid(hash);
            }
        }
    }
}