using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        
        IBookingRepository Bookings { get; }
        ITripRepository Trips { get; }
        IRouteRepository Routes { get; }
        IBusRepository Buses { get; }
        IDriverRepository Drivers { get; }
        IUserRepository Users { get; }
        INotificationRepository Notifications { get; }  
        IReviewRepository Reviews { get; }  
        IPaymentRepository Payments { get; }  
        IDashboardRepository Dashboard { get; }

        IChatRepository Chat { get; }
      

        Task<int> SaveChangesAsync();
    }
}
