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
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<Driver>> GetAvailableDriversAsync()
            => await _ctx.Drivers
                .Include(d => d.User)
                .Where(d => d.IsAvailable)
                .ToListAsync();

        public async Task<IEnumerable<Driver>> GetDriversAsync()
            => await _ctx.Drivers
                .Include(d => d.User)
                .ToListAsync();



        public async Task<Driver?> GetDriverWithUserAsync(Guid driverId)
            => await _ctx.Drivers
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == driverId);

        public async Task<Driver?> GetDriverByUserIdAsync(Guid userId)
            => await _ctx.Drivers
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);

    }
}
