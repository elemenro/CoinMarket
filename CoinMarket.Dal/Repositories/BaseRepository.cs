using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinMarket.Dal.Interfaces;
using CoinMarket.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Dal.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly MyContext _context;
        private DbSet<T> entities;
        public BaseRepository(MyContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsNoTracking();
        }
        public async Task<T> Get(params object[] args)
        {
            return await entities.FindAsync(args);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await entities.AddAsync(entity);
        }
        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entities.Update(entity);
        }
        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
        }
    }
}