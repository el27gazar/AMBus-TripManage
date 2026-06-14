using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetBookingById
{
    public record GetBookingByIdQuery(
        Guid BookingId,
        Guid UserId,
        bool IsAdmin = false
    ) : IRequest<BookingDto>;
}
