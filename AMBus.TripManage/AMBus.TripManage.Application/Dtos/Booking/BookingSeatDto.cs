using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Booking
{
    public class BookingSeatDto
    {
       public Guid Id { get; set; }

       public Guid SeatId { get;set; }
       public string SeatNumber { get; set; }
       public string PassengerName { get; set; }
      public string? PassengerIdNumber { get; set; }
    }
}
