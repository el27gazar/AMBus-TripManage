using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
{
    public interface ISystemNotificationService
    {
        // Booking events
        Task NotifyBookingConfirmedAsync(Guid bookingId);
        Task NotifyBookingCancelledAsync(Guid bookingId, Guid cancelledByUserId);
        Task NotifyBookingPendingPaymentAsync(Guid bookingId);

        // Payment events
        Task NotifyPaymentReceivedAsync(Guid bookingId, decimal amount);
        Task NotifyPaymentFailedAsync(Guid bookingId);
        Task NotifyRefundProcessedAsync(Guid bookingId, decimal amount);

        // Trip events
        Task NotifyTripStartedAsync(Guid tripId);
        Task NotifyTripCancelledAsync(Guid tripId, string reason);
        Task NotifyTripCompletedAsync(Guid tripId);
        Task NotifyTripReminderAsync(Guid tripId);  // قبل الرحلة بساعة
    }
}
