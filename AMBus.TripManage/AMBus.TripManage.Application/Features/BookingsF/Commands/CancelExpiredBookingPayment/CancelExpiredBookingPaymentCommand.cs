using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CancelExpiredBookingPayment
{
    public record CancelExpiredBookingPaymentCommand(string SessionId) : IRequest<Unit>;

}
