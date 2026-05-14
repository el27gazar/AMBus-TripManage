using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class BookingSeat: AuditableEntity
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? PassengerName { get; set; }

        [MaxLength(20)]
        public string? PassengerIdNumber { get; set; }   // national ID / passport

        // FKs
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public Guid SeatId { get; set; }
        public Seat Seat { get; set; } = null!;
    }
}
