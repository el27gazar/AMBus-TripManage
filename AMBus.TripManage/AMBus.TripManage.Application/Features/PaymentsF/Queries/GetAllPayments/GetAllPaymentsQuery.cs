using AMBus.TripManage.Application.Dtos.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetAllPayments
{
    public record GetAllPaymentsQuery(
         string? Method,
         string? Status,
         DateTime? From,
         DateTime? To,
         int Page = 1,
         int PageSize = 20
     ) : IRequest<PagedPaymentsDto>;
}
