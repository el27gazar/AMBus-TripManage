using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.BookingDto
{
    public record BookingSeatDto(
        Guid Id,
        Guid SeatId,
        string SeatNumber,
        string PassengerName,
        string? PassengerIdNumber
    );
}
