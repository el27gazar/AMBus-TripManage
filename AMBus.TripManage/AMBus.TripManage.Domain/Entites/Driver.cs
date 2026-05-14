using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Driver: AuditableEntity
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string LicenseNumber { get; set; } = string.Empty;

        public DateTime LicenseExpiry { get; set; }

        [MaxLength(20)]
        public string? EmergencyContact { get; set; }

        public bool IsAvailable { get; set; } = true;

        // FK (one-to-one with User)
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
