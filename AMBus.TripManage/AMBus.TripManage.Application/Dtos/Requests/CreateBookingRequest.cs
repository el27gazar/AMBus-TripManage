using AMBus.TripManage.Application.Dtos.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public record CreateBookingRequest(
           Guid TripId,
           List<BookingSeatDto> Seats);
}
