
    using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
    using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands;
using AMBus.TripManage.Domain.Entites;
    using MediatR;
    using System.Text.Json;

    namespace AMBus.TripManage.Application.Features.BookingsF.Commands.InitiateBookingPayment
    {
        public class InitiateBookingPaymentCommandHandler
            : IRequestHandler<InitiateBookingPaymentCommand, InitiateBookingPaymentResultDto>
        {
            private readonly IUnitOfWork _uow;
            private readonly IPaymentService _paymentService;

            public InitiateBookingPaymentCommandHandler(IUnitOfWork uow, IPaymentService paymentService)
            {
                _uow = uow;
                _paymentService = paymentService;
            }

            public async Task<InitiateBookingPaymentResultDto> Handle(
                InitiateBookingPaymentCommand request, CancellationToken ct)
            {
                var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                    ?? throw new NotFoundException(nameof(Trip), request.TripId);

                if (trip.Status != TripStatus.Scheduled)
                    throw new BusinessRuleException("لا يمكن الحجز في هذه الرحلة.");

                if (trip.AvailableSeats < request.Seats.Count)
                    throw new BusinessRuleException("لا يوجد عدد مقاعد كافٍ.");

                foreach (var s in request.Seats)
                {
                    var taken = await _uow.Bookings.IsSeatAlreadyBookedAsync(s.SeatId, request.TripId);
                    if (taken)
                        throw new ConflictException("أحد المقاعد المختارة محجوز بالفعل.");
                }

                var passenger = await _uow.Users.GetByIdAsync(request.UserId)
                    ?? throw new NotFoundException(nameof(User), request.UserId);

                var totalPrice = trip.BasePrice * request.Seats.Count;

                var bookingPayload = new PendingBookingPayload(
                    TripId: request.TripId,
                    UserId: request.UserId,
                    SeatIds: request.Seats.Select(s => s.SeatId).ToList(),
                    TotalPrice: totalPrice,
                    PassengerName: passenger.FullName,
                   
                    PhoneNumber: request.PhoneNumber);

                if (request.PaymentMethod == "Cash")
                {
                    var booking = await CreateConfirmedOrPendingBookingAsync(
                        bookingPayload, PaymentMethod.Cash, PaymentProvider.Manual,
                        PaymentStatus.PendingCustomerAction, ct);

                    return new InitiateBookingPaymentResultDto(
                        true,
                        "تم إنشاء الحجز. الدفع كاش سيتم تأكيده من الإدارة.",
                        "show_reference",
                        null,
                        booking.Id);
                }

                var checkoutResult = await _paymentService.CreateCheckoutSessionAsync(
                    new CreateCheckoutRequest(
                        Amount: totalPrice,
                        Currency: "EGP",
                        CustomerEmail: passenger.Email!,
                        CustomerName: passenger.FullName,
                        Metadata: BuildMetadata(bookingPayload)));

                if (!checkoutResult.Success)
                    throw new BusinessRuleException($"فشل إنشاء جلسة الدفع: {checkoutResult.Error}");

                return new InitiateBookingPaymentResultDto(
                    true,
                    "أكمل الدفع لتأكيد حجزك.",
                    "redirect",
                    checkoutResult.CheckoutUrl,
                    null);
            }

            private static Dictionary<string, string> BuildMetadata(PendingBookingPayload payload) => new()
        {
            { "tripId", payload.TripId.ToString() },
            { "userId", payload.UserId.ToString() },
            { "seatIds", string.Join(",", payload.SeatIds) },
            { "totalPrice", payload.TotalPrice.ToString() },
            { "passengerName", payload.PassengerName },
            { "phoneNumber", payload.PhoneNumber ?? "" }
        };

            private async Task<Booking> CreateConfirmedOrPendingBookingAsync(
                PendingBookingPayload payload, PaymentMethod method,
                PaymentProvider provider, PaymentStatus status, CancellationToken ct)
            {
                var now = DateTime.UtcNow;
                var uid = payload.UserId.ToString();

                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    TripId = payload.TripId,
                    UserId = payload.UserId,
                    Status = BookingStatus.Pending,
                    TotalPrice = payload.TotalPrice,
                    BookedAt = now,
                    QrCode = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                    BookingSeats = payload.SeatIds.Select(seatId => new BookingSeat
                    {
                        Id = Guid.NewGuid(),
                        SeatId = seatId,
                        PassengerName = payload.PassengerName,
                        CreatedDate = now
                    }).ToList(),
                    CreatedBy = uid,
                    CreatedDate = now,
                    LastModifiedBy = uid,
                    LastModifiedDate = now
                };

                await _uow.Bookings.AddAsync(booking);

                var trip = await _uow.Trips.GetByIdAsync(payload.TripId);
                if (trip != null)
                {
                    trip.AvailableSeats -= payload.SeatIds.Count;
                    _uow.Trips.Update(trip);
                }

                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    BookingId = booking.Id,
                    Amount = payload.TotalPrice,
                    Currency = "EGP",
                    Method = method,
                    Provider = provider,
                    Status = status,
                    CreatedBy = uid,
                    CreatedDate = now,
                    LastModifiedBy = uid,
                    LastModifiedDate = now
                };

                await _uow.Payments.AddAsync(payment);
                await _uow.SaveChangesAsync();

                return booking;
            }
        }

        public record PendingBookingPayload(
            Guid TripId, Guid UserId, List<Guid> SeatIds, decimal TotalPrice,
            string PassengerName, string? PhoneNumber);
    }

