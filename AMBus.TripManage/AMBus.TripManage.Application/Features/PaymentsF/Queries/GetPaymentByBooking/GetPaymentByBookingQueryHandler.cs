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

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentByBooking
{
    public class GetPaymentByBookingQueryHandler
            : IRequestHandler<GetPaymentByBookingQuery, PaymentDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetPaymentByBookingQueryHandler(IUnitOfWork uow, IMapper mapper)
        { _uow = uow; _mapper = mapper; }

        public async Task<PaymentDto?> Handle(
            GetPaymentByBookingQuery q, CancellationToken ct)
        {
            var booking = await _uow.Bookings.GetByIdAsync(q.BookingId)
                ?? throw new NotFoundException(nameof(Booking), q.BookingId);

            if (!q.IsAdmin && booking.UserId != q.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية.");

            var p = await _uow.Payments.GetByBookingAsync(q.BookingId);
            return p is null ? null : _mapper.Map<PaymentDto>(p);
        }
    }
}
