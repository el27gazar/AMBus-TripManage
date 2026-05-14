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
    public class BusRepository : GenericRepository<Bus>, IBusRepository
    {
        public BusRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Bus?> GetBusWithSeatsAsync(Guid busId)
            => await _ctx.Buses
                .Include(b => b.Seats.OrderBy(s => s.SeatNumber))
                .FirstOrDefaultAsync(b => b.Id == busId);
    }
}
