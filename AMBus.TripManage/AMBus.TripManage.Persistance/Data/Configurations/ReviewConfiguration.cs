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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Comment)
             .HasMaxLength(500);

            b.Property(x => x.CreatedAt)
             .HasDefaultValueSql("GETUTCDATE()");

            b.HasOne(r => r.User)
             .WithMany(u => u.Reviews)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(r => r.Trip)
             .WithMany(t => t.Reviews)
             .HasForeignKey(r => r.TripId)
             .OnDelete(DeleteBehavior.Restrict);
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }

}
