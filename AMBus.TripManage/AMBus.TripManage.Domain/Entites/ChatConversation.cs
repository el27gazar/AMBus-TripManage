using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public enum ConversationStatus
    {
        Open,       // مفتوحة
        InProgress, // Admin بيرد
        Closed      // مغلقة
    }
    public class ChatConversation : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // الـ User صاحب المحادثة
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // الـ Admin/Support المسؤول (null = لسه محدش اتعين)
        public Guid? AdminId { get; set; }
        public User? Admin { get; set; }

        public ConversationStatus Status { get; set; }
            = ConversationStatus.Open;

        [MaxLength(200)]
        public string? Subject { get; set; }    // موضوع المحادثة

        public ICollection<ChatMessage> Messages { get; set; }
            = new List<ChatMessage>();
    }
    public class ChatMessage : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ConversationId { get; set; }
        public ChatConversation Conversation { get; set; } = null!;

        public Guid SenderId { get; set; }
        public User Sender { get; set; } = null!;

        [Required, MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
    }

}
