using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.SeatDto
{
    public class SeatDto
    {
        public Guid Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public Guid BusId { get; set; }
    }
}
