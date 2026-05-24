using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Route : AuditableEntity
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Stop> Stops { get; set; } = new List<Stop>();
        public ICollection<Trip> DepartureTrips { get; set; } = new List<Trip>();
        public ICollection<Trip> ArrivalTrips { get; set; } = new List<Trip>();
    }
}
