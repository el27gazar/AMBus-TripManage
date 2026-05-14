using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(decimal amount, string method, Guid bookingId);
        Task<bool> RefundAsync(string transactionId);
    }
}
