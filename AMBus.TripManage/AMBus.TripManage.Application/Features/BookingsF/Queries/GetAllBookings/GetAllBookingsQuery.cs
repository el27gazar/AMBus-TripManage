using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetAllBookings
{
    public record GetAllBookingsQuery(
       string? Status,
       Guid? UserId,
       Guid? TripId,
       int Page = 1,
       int PageSize = 20
   ) : IRequest<PagedResultDto<BookingDto>>;
}
