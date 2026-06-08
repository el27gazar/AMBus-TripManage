using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IDriverRepository : IGenericRepository<Driver>
    {
        Task<IEnumerable<Driver>> GetAvailableDriversAsync();
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<Driver?> GetDriverWithUserAsync(Guid driverId);
        Task<Driver?> GetDriverByUserIdAsync(Guid userId);
    }
}
