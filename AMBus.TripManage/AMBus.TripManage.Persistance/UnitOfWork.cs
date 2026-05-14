using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Persistance.Data;
using AMBus.TripManage.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;

        public IBookingRepository Bookings { get; }
        public ITripRepository Trips { get; }
        public IRouteRepository Routes { get; }
        public IBusRepository Buses { get; }
        public IDriverRepository Drivers { get; }
        public IUserRepository Users { get; }
        public INotificationRepository Notifications { get; }
        public IReviewRepository Reviews { get; }
        public IPaymentRepository Payments { get; }
        public IDashboardRepository Dashboard { get; }

        public UnitOfWork(AppDbContext ctx)
        {
            _ctx = ctx;
            Bookings = new BookingRepository(ctx);
            Trips = new TripRepository(ctx);
            Routes = new RouteRepository(ctx);
            Buses = new BusRepository(ctx);
            Drivers = new DriverRepository(ctx);
            Users = new UserRepository(ctx);
            Notifications = new NotificationRepository(ctx);
            Reviews = new ReviewRepository(ctx);
            Payments = new PaymentRepository(ctx);
            Dashboard = new DashboardRepository(ctx);
        }

        public async Task<int> SaveChangesAsync()
            => await _ctx.SaveChangesAsync();

        public void Dispose() => _ctx.Dispose();
    }
}
