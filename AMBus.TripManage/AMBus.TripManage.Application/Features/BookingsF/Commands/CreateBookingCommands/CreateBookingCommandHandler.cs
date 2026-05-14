using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.BookingDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public class CreateBookingCommandHandler
     : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISystemNotificationService _notifications;

        public CreateBookingCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ISystemNotificationService notifications)
        {
            _uow = uow;
            _mapper = mapper;
            _notifications = notifications;
        }

        public async Task<BookingDto> Handle(
            CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            if (trip.Status != TripStatus.Scheduled)
                throw new BusinessRuleException("لا يمكن الحجز — الرحلة غير متاحة.");

            if (trip.AvailableSeats < request.Seats.Count)
                throw new BusinessRuleException(
                    $"المقاعد المتاحة ({trip.AvailableSeats}) أقل من المطلوبة.");

            foreach (var s in request.Seats)
            {
                var taken = await _uow.Bookings
                    .IsSeatAlreadyBookedAsync(s.SeatId, request.TripId);
                if (taken)
                    throw new ConflictException("أحد المقاعد محجوز بالفعل.");
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                TripId = request.TripId,
                TotalPrice = trip.BasePrice * request.Seats.Count,
                Status = BookingStatus.Pending,
                CreatedDate = DateTime.UtcNow,
                QrCode = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                BookingSeats = request.Seats.Select(s => new BookingSeat
                {
                    Id = Guid.NewGuid(),
                    SeatId = s.SeatId,
                    PassengerName = s.PassengerName,
                    PassengerIdNumber = s.PassengerIdNumber,
                    CreatedDate = DateTime.UtcNow
                }).ToList()
            };

            trip.AvailableSeats -= request.Seats.Count;
            trip.LastModifiedDate = DateTime.UtcNow;
            _uow.Trips.Update(trip);

            await _uow.Bookings.AddAsync(booking);
            await _uow.SaveChangesAsync();

            // ── إشعار أوتوماتيكي ──────────────────────
            await _notifications.NotifyBookingPendingPaymentAsync(booking.Id);

            var created = await _uow.Bookings
                .GetBookingWithDetailsAsync(booking.Id) ?? booking;

            return _mapper.Map<BookingDto>(created);
        }
    }
}
