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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Message)
             .HasMaxLength(300).IsRequired();

            b.Property(x => x.Type)
             .HasConversion<string>()
             .HasMaxLength(30);

            b.Property(x => x.IsRead)
             .HasDefaultValue(false);

            b.Property(x => x.CreatedAt)
             .HasDefaultValueSql("GETUTCDATE()");

            b.HasOne(n => n.User)
             .WithMany(u => u.Notifications)
             .HasForeignKey(n => n.UserId)
             .OnDelete(DeleteBehavior.Restrict);
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }
}
