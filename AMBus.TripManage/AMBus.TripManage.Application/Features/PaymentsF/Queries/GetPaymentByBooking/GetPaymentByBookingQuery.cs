using AMBus.TripManage.Application.Dtos.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentByBooking
{
    public record GetPaymentByBookingQuery(Guid BookingId, Guid UserId, bool IsAdmin = false)
           : IRequest<PaymentDto?>;
}
