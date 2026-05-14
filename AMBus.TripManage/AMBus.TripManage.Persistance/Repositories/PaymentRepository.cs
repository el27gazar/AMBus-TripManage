using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Payment?> GetPaymentByBookingAsync(Guid bookingId)
            => await _ctx.Payments
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);

        public async Task<IEnumerable<Payment>> GetPaidPaymentsInRangeAsync(
            DateTime from, DateTime to)
            => await _ctx.Payments
                .Where(p =>
                    p.Status == PaymentStatus.Paid &&
                    p.PaidAt >= from &&
                    p.PaidAt <= to)
                .OrderBy(p => p.PaidAt)
                .ToListAsync();
    }
}
