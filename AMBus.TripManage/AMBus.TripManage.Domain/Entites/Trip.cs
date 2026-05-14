using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public enum TripStatus { Scheduled, InProgress, Completed, Cancelled }

    public class Trip: AuditableEntity
    {
        public Guid Id { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal BasePrice { get; set; }

        public TripStatus Status { get; set; } = TripStatus.Scheduled;
        public int AvailableSeats { get; set; }

        // FKs
        public Guid RouteId { get; set; }
        public Route Route { get; set; } = null!;

        public Guid BusId { get; set; }
        public Bus Bus { get; set; } = null!;

        public Guid DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        // Navigation
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
