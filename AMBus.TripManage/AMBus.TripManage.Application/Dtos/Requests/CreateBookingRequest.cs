using AMBus.TripManage.Application.Dtos.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class CreateBookingRequest
    {
        public Guid TripId { get; set; }
        public List<BookingSeatDto> Seats { get; set; }
    }
}