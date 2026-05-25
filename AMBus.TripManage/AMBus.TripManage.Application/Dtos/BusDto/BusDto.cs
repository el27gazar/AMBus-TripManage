using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.BusDto
{
    public class BusDto
    {
     

        public Guid Id { get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public int TotalSeats { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
