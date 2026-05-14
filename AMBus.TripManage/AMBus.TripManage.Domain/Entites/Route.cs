using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Route: AuditableEntity
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string FromCity { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string ToCity { get; set; } = string.Empty;

        public int EstimatedDurationMinutes { get; set; }
        public double DistanceKm { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Stop> Stops { get; set; } = new List<Stop>();
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
