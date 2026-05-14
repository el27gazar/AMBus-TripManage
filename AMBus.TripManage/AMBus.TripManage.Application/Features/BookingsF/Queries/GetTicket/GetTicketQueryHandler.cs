using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TicketDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetTicket
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, TicketDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TicketDto> Handle(
            GetTicketQuery request,
            CancellationToken cancellationToken)
        {
            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(request.BookingId)
                ?? throw new NotFoundException(nameof(Booking), request.BookingId);

            if (!request.IsAdmin && booking.UserId != request.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية عرض هذه التذكرة.");

            if (booking.Status == BookingStatus.Cancelled)
                throw new BusinessRuleException("لا يمكن عرض تذكرة حجز ملغي.");

            return _mapper.Map<TicketDto>(booking);
        }
    }
}
