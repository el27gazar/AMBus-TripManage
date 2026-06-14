using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Payment;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class PaymentRepository
        : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Payment?> GetByBookingAsync(Guid bookingId)
            => await _ctx.Payments
                .OrderByDescending(p => p.CreatedDate)
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);

        public async Task<Payment?> GetWithBookingAsync(Guid paymentId)
            => await _ctx.Payments
                .Include(p => p.Booking)
                    .ThenInclude(b => b.BookingSeats)
                .FirstOrDefaultAsync(p => p.Id == paymentId);

        public async Task<Payment?> GetByTransactionIdAsync(string transactionId)
            => await _ctx.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p =>
                    p.StripeClientSecret == transactionId ||
                    p.ExternalTransactionId == transactionId);

        public async Task<Payment?> GetByOrderIdAsync(string orderId)
            => await _ctx.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.ReferenceNumber == orderId);

        public async Task<IEnumerable<Payment>> GetPaidInRangeAsync(
            DateTime from, DateTime to)
            => await _ctx.Payments
                .Where(p =>
                    p.Status == PaymentStatus.Paid &&
                    p.PaidAt >= from &&
                    p.PaidAt <= to)
                .OrderBy(p => p.PaidAt)
                .ToListAsync();

        public async Task<(IEnumerable<Payment> Items,
                           int Total,
                           PaymentSummaryDto Summary)>
            GetPagedAsync(PaymentFilter filter)
        {
            var query = _ctx.Payments
                .Include(p => p.Booking)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Method) &&
                Enum.TryParse<PaymentMethod>(filter.Method, out var m))
                query = query.Where(p => p.Method == m);

            if (!string.IsNullOrEmpty(filter.Status) &&
                Enum.TryParse<PaymentStatus>(filter.Status, out var s))
                query = query.Where(p => p.Status == s);

            if (filter.From.HasValue)
                query = query.Where(p => p.CreatedDate >= filter.From.Value);

            if (filter.To.HasValue)
                query = query.Where(p => p.CreatedDate <= filter.To.Value);

            var total = await query.CountAsync();

            var summary = new PaymentSummaryDto(
                TotalCount: total,
                TotalPaidAmount: await query.Where(p => p.Status == PaymentStatus.Paid)
                                          .SumAsync(p => (decimal?)p.Amount) ?? 0,
                TotalRefundedAmount: await query.Where(p => p.Status == PaymentStatus.Refunded)
                                          .SumAsync(p => (decimal?)p.Amount) ?? 0,
                PaidCount: await query.CountAsync(p => p.Status == PaymentStatus.Paid),
                FailedCount: await query.CountAsync(p => p.Status == PaymentStatus.Failed),
                PendingCount: await query.CountAsync(p =>
                                    p.Status == PaymentStatus.Pending ||
                                    p.Status == PaymentStatus.PendingCustomerAction),
                RefundedCount: await query.CountAsync(p => p.Status == PaymentStatus.Refunded)
            );

            var items = await query
                .OrderByDescending(p => p.CreatedDate)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (items, total, summary);
        }
    }
}
