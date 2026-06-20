using AMBus.TripManage.Application.Dtos.Payment;
using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment?> GetByBookingAsync(Guid bookingId);
        Task<Payment?> GetWithBookingAsync(Guid paymentId);

        Task<Payment?> GetByExternalTransactionAsync(string externalTransactionId);
        Task<IEnumerable<Payment>> GetPaidInRangeAsync(DateTime from, DateTime to);
        Task<(IEnumerable<Payment> Items, int Total, PaymentSummaryDto Summary)>
            GetPagedAsync(PaymentFilter filter);

        Task<List<Payment>> GetExpiredPendingPaymentsAsync(DateTime cutoffDate);


    }
   
    public class PaymentFilter
    {
        public string? Method { get; init; }
        public string? Status { get; init; }
        public DateTime? From { get; init; }
        public DateTime? To { get; init; }
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
