using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public enum BookingStatus { Pending, Confirmed, Cancelled, Completed }

    public class Booking: AuditableEntity
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(20)]
        public string? QrCode { get; set; }      // for ticket validation

        // FKs
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;

        // Navigation
        public Payment? Payment { get; set; }
        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}
