using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands
{
    public record ConfirmBookingFromStripeCommand(
        string SessionId, string PaymentIntentId, IDictionary<string, string> Metadata
    ) : IRequest<Unit>;
}
