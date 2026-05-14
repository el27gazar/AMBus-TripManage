using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public enum BusType { Standard, VIP, MiniCoach }

    public class Bus: AuditableEntity
    {
        public Guid Id { get; set; }

        [Required, MaxLength(20)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Model { get; set; } = string.Empty;

        public int TotalSeats { get; set; }
        public BusType Type { get; set; } = BusType.Standard;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }

}
