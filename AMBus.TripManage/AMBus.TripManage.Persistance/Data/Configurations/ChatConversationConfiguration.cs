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
    public class ChatConversationConfiguration
          : IEntityTypeConfiguration<ChatConversation>
    {
        public void Configure(EntityTypeBuilder<ChatConversation> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Subject)
             .HasMaxLength(200);

            b.Property(x => x.Status)
             .HasConversion<string>()
             .HasMaxLength(20);

            b.Property(x => x.CreatedBy)
             .HasMaxLength(100);

            b.Property(x => x.LastModifiedBy)
             .HasMaxLength(100);

            // ── User صاحب المحادثة ────────────────────────
            b.HasOne(c => c.User)
             .WithMany()
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            // ── Admin المسؤول (nullable) ───────────────────
            b.HasOne(c => c.Admin)
             .WithMany()
             .HasForeignKey(c => c.AdminId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
