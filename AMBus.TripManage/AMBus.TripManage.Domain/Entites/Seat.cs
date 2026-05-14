using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Seat: AuditableEntity
    {
        public Guid Id { get; set; }

        [Required, MaxLength(10)]
        public string SeatNumber { get; set; } = string.Empty;  //  "A1", "B3"

        public bool IsAvailable { get; set; } = true;

        // FK
        public Guid BusId { get; set; }
        public Bus Bus { get; set; } = null!;

        // Navigation
        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}
