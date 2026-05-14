using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Stop: AuditableEntity
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string CityName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? StationAddress { get; set; }

        public int StopOrder { get; set; }       // 1, 2, 3 … order along the route
        public int ArrivalOffsetMinutes { get; set; }  // minutes after departure

        // FK
        public Guid RouteId { get; set; }
        public Route Route { get; set; } = null!;
    }
}
