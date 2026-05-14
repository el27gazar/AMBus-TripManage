using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.BookingDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetMyBookings
{
    public record GetMyBookingsQuery(
       Guid UserId,
       string? Status,
       int Page = 1,
       int PageSize = 10
   ) : IRequest<PagedResultDto<BookingDto>>;

}
