using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands
{
    public class ConfirmBookingCommandHandler
       : IRequestHandler<ConfirmBookingCommand, BookingDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISystemNotificationService _Notification;

        public ConfirmBookingCommandHandler(IUnitOfWork uow, IMapper mapper,ISystemNotificationService notificationService)
        {
            _uow = uow;
            _mapper = mapper;
            _Notification = notificationService;
        }

        public async Task<BookingDto> Handle(
            ConfirmBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(request.BookingId)
                ?? throw new NotFoundException(nameof(Booking), request.BookingId);

            if (booking.Status != BookingStatus.Pending)
                throw new BusinessRuleException("يمكن تأكيد الحجوزات المعلقة فقط.");

            booking.Status = BookingStatus.Confirmed;
            booking.LastModifiedDate = DateTime.UtcNow;

            await _Notification.NotifyBookingConfirmedAsync(bookingId: booking.Id);

            _uow.Bookings.Update(booking);
            await _uow.SaveChangesAsync();

            return _mapper.Map<BookingDto>(booking);
        }
    }
}