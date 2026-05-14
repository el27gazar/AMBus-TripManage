using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Review: AuditableEntity
    {
        public Guid Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FKs
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid TripId { get; set; }
        public Trip Trip { get; set; } = null!;
        
    }
}
