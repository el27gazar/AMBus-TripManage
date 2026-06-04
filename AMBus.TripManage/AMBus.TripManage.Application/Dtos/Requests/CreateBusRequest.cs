using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class CreateBusRequest
    {
       public string PlateNumber { get; set; }
       public string Model { get; set; }
       public int TotalSeats { get; set; }
       public string Type { get; set; }
    }
    public class UpdateBusRequest
    {
        public string Model { get; set; }
        public bool IsActive { get; set; }

        public string PlateNumber { get; set; } = string.Empty;
    }
}   