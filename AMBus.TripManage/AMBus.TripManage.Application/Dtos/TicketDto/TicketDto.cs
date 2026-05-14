using AMBus.TripManage.Application.Dtos.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.TicketDto
{
    public record TicketDto(
            Guid BookingId,
            string QrCode,
            string FromCity,
            string ToCity,
            DateTime DepartureTime,
            string BusPlate,
            string BusType,
            List<BookingSeatDto> Passengers
        );
}
