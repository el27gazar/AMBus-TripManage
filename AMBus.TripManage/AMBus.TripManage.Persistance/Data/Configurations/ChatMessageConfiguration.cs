using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Data.Configurations
{
    public class ChatMessageConfiguration
         : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Content)
             .HasMaxLength(2000)
             .IsRequired();

            b.Property(x => x.CreatedBy)
             .HasMaxLength(100);

            b.Property(x => x.LastModifiedBy)
             .HasMaxLength(100);

            // ── Conversation ──────────────────────────────
            b.HasOne(m => m.Conversation)
             .WithMany(c => c.Messages)
             .HasForeignKey(m => m.ConversationId)
             .OnDelete(DeleteBehavior.Cascade);  // حذف المحادثة = حذف رسائلها

            // ── Sender ────────────────────────────────────
            b.HasOne(m => m.Sender)
             .WithMany()
             .HasForeignKey(m => m.SenderId)
             .OnDelete(DeleteBehavior.Restrict);

            // ── Index عشان الـ query بيكون كتير على ConversationId ──
            b.HasIndex(m => m.ConversationId);
            b.HasIndex(m => new { m.ConversationId, m.IsRead });
        }
    }
}
