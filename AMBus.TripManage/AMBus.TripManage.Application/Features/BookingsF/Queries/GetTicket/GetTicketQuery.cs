using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TicketDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetTicket
{
    public record GetTicketQuery(
       Guid BookingId,
       Guid UserId,
       bool IsAdmin = false
   ) : IRequest<TicketDto>;
}
