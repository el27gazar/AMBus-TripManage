using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.BookingDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands
{
    public record ConfirmBookingCommand(Guid BookingId) : IRequest<BookingDto>;
}
