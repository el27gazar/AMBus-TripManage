using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CancelBooking
{
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ISystemNotificationService _notifications;

        public CancelBookingCommandHandler(
            IUnitOfWork uow,
            ISystemNotificationService notifications)
        {
            _uow = uow;
            _notifications = notifications;
        }

        public async Task<Unit> Handle(
            CancelBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await _uow.Bookings
                .GetBookingWithDetailsAsync(request.BookingId)
                ?? throw new NotFoundException(nameof(Booking), request.BookingId);

            if (!request.IsAdmin && booking.UserId != request.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية إلغاء هذا الحجز.");

            if (booking.Status == BookingStatus.Cancelled)
                throw new BusinessRuleException("الحجز ملغي بالفعل.");

            if (booking.Status == BookingStatus.Completed)
                throw new BusinessRuleException("لا يمكن إلغاء حجز مكتمل.");

            if (booking.Status == BookingStatus.Confirmed)
                throw new BusinessRuleException(
                    "هذا الحجز مدفوع بالفعل. لإلغائه، استخدم خاصية استرداد المبلغ (Refund) بدل الإلغاء المباشر.");

            booking.Status = BookingStatus.Cancelled;
            booking.LastModifiedDate = DateTime.UtcNow;

            var trip = await _uow.Trips.GetByIdAsync(booking.TripId)
                ?? throw new NotFoundException(nameof(Trip), booking.TripId);

            trip.AvailableSeats += booking.BookingSeats.Count;
            trip.LastModifiedDate = DateTime.UtcNow;
            _uow.Trips.Update(trip);

            _uow.Bookings.Update(booking);
            await _uow.SaveChangesAsync();

            await _notifications.NotifyBookingCancelledAsync(
                booking.Id, request.UserId);

            return Unit.Value;
        }
    }
}