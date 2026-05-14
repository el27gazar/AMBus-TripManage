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
        Task<Payment?> GetPaymentByBookingAsync(Guid bookingId);
        Task<IEnumerable<Payment>> GetPaidPaymentsInRangeAsync(
            DateTime from, DateTime to);
    }
}
