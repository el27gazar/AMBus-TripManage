using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.SeatDto
{
    public class SeatAvailabilityDto
    {
        public Guid SeatId { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}