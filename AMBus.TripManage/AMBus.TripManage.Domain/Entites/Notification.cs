using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public enum NotificationType { BookingConfirmed, TripReminder, TripCancelled, PaymentReceived, General }

    public class Notification : AuditableEntity
    {
        public Guid Id { get; set; }

        public NotificationType Type { get; set; }

        [Required, MaxLength(300)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FK
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
