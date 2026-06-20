using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Domain.Entites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class CleanupExpiredCardPaymentsJob
    {
        private readonly IUnitOfWork _uow;
        private readonly IPaymentService _paymentService;
        private readonly ISystemNotificationService _notif;
        private readonly ILogger<CleanupExpiredCardPaymentsJob> _logger;

        public CleanupExpiredCardPaymentsJob(
            IUnitOfWork uow,
            IPaymentService paymentService,
            ISystemNotificationService notif,
            ILogger<CleanupExpiredCardPaymentsJob> logger)
        {
            _uow = uow;
            _paymentService = paymentService;
            _notif = notif;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            // مدة صلاحية Stripe Checkout Session هي 30 دقيقة (CreateCheckoutSessionAsync)
            // بنسيب 5 دقائق هامش أمان قبل ما نعتبرها منتهية فعليًا
            var cutoff = DateTime.UtcNow.AddMinutes(-35);

            var expiredPayments = await _uow.Payments.GetExpiredPendingPaymentsAsync(cutoff);

            if (expiredPayments.Count == 0)
            {
                _logger.LogInformation("Cleanup job: لا توجد دفعات معلّقة منتهية.");
                return;
            }

            _logger.LogInformation("Cleanup job: تم العثور على {Count} دفعة معلّقة منتهية.", expiredPayments.Count);

            foreach (var payment in expiredPayments)
            {
                try
                {
                    await ProcessSinglePaymentAsync(payment);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Cleanup job: فشل معالجة الدفعة {PaymentId}.", payment.Id);
                }
            }
        }

        private async Task ProcessSinglePaymentAsync(Payment payment)
        {
            if (string.IsNullOrEmpty(payment.ExternalTransactionId))
            {
                await CancelAndReleaseAsync(payment);
                return;
            }

            // قبل الإلغاء، نتأكد من Stripe مباشرة - تحسبًا لو العميل دفع فعليًا
            // بس الـ Redirect لصفحة Success فشل أو قفل الصفحة بدري
            var verify = await _paymentService.VerifyCheckoutSessionAsync(payment.ExternalTransactionId);

            if (verify.Success && verify.Status == "paid")
                await ConfirmLatePaymentAsync(payment, verify.PaymentIntentId);
            else
                await CancelAndReleaseAsync(payment);
        }

        private async Task ConfirmLatePaymentAsync(Payment payment, string? paymentIntentId)
        {
            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(payment.BookingId);
            if (booking is null || booking.Status == BookingStatus.Confirmed) return;

            var now = DateTime.UtcNow;

            payment.Status = PaymentStatus.Paid;
            payment.PaidAt = now;
            payment.StripePaymentIntentId = paymentIntentId;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            booking.Status = BookingStatus.Confirmed;
            booking.LastModifiedDate = now;
            _uow.Bookings.Update(booking);

            await _uow.SaveChangesAsync();

            await _notif.NotifyPaymentReceivedAsync(booking.Id, payment.Amount);
            await _notif.NotifyBookingConfirmedAsync(booking.Id);

            _logger.LogInformation(
                "Cleanup job: تأكيد متأخر للدفعة {PaymentId} - العميل دفع فعليًا لكن الـ Redirect فشل.",
                payment.Id);
        }

        private async Task CancelAndReleaseAsync(Payment payment)
        {
            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(payment.BookingId);
            if (booking is null || booking.Status == BookingStatus.Cancelled) return;

            var now = DateTime.UtcNow;

            payment.Status = PaymentStatus.Failed;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            booking.Status = BookingStatus.Cancelled;
            booking.LastModifiedDate = now;
            _uow.Bookings.Update(booking);

            var trip = await _uow.Trips.GetByIdAsync(booking.TripId);
            if (trip != null)
            {
                trip.AvailableSeats += booking.BookingSeats?.Count ?? 0;
                _uow.Trips.Update(trip);
            }

            await _uow.SaveChangesAsync();

            _logger.LogInformation(
                "Cleanup job: تم إلغاء الحجز {BookingId} وتحرير المقاعد (انتهت صلاحية جلسة الدفع).",
                booking.Id);
        }
    }
 }
