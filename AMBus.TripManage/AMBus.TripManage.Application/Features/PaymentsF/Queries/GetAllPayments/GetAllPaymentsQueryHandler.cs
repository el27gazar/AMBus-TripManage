using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Payment;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetAllPayments
{
    public class GetAllPaymentsQueryHandler
           : IRequestHandler<GetAllPaymentsQuery, PagedPaymentsDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllPaymentsQueryHandler(IUnitOfWork uow, IMapper mapper)
        { _uow = uow; _mapper = mapper; }

        public async Task<PagedPaymentsDto> Handle(
            GetAllPaymentsQuery q, CancellationToken ct)
        {
            var (items, total, summary) = await _uow.Payments.GetPagedAsync(
                new PaymentFilter
                {
                    Method = q.Method,
                    Status = q.Status,
                    From = q.From,
                    To = q.To,
                    Page = q.Page,
                    PageSize = q.PageSize
                });

            return new PagedPaymentsDto(
                Items: _mapper.Map<List<PaymentDto>>(items),
                TotalCount: total,
                Page: q.Page,
                PageSize: q.PageSize,
                TotalPages: (int)Math.Ceiling(total / (double)q.PageSize),
                Summary: summary);
        }
    }
}
