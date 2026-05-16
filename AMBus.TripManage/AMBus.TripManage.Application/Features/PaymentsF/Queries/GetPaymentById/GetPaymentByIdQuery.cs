using AMBus.TripManage.Application.Dtos.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery(Guid PaymentId, Guid UserId, bool IsAdmin = false)
        : IRequest<PaymentDto>;
}
