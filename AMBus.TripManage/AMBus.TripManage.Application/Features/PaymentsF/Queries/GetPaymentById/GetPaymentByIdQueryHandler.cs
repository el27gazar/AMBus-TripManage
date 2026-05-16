using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.Payment;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentById
{
    public class GetPaymentByIdQueryHandler
           : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetPaymentByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        { _uow = uow; _mapper = mapper; }

        public async Task<PaymentDto> Handle(
            GetPaymentByIdQuery q, CancellationToken ct)
        {
            var p = await _uow.Payments.GetWithBookingAsync(q.PaymentId)
                ?? throw new NotFoundException(nameof(Payment), q.PaymentId);

            if (!q.IsAdmin && p.Booking?.UserId != q.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية.");

            return _mapper.Map<PaymentDto>(p);
        }
    }
}
