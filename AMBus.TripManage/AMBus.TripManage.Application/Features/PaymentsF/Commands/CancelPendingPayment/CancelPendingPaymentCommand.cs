using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.CancelPendingPayment
{
    public record CancelPendingPaymentCommand(
           Guid BookingId,
           Guid UserId
       ) : IRequest<Unit>;
}
