using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _set;

        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _set = ctx.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
            => await _set.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _set.ToListAsync();

        public async Task AddAsync(T entity)
            => await _set.AddAsync(entity);

        public void Update(T entity)
            => _set.Update(entity);

        public void Delete(T entity)
            => _set.Remove(entity);
    }
}
