using FlapKap.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlapKap.Infrastructure.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly VendingMachieneContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(VendingMachieneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();

        }
        public IEnumerable<T> GetData(Expression<Func<T,bool>>? predicate)
        {
            var query = _entities.AsQueryable();
            if(predicate != null)
                query = query.Where(predicate);
            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>>? predicate = null)
        {
            var query = _entities.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public T Add(T entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _entities.Remove(entity);
            return entity;
        }

        public IQueryable<T> Query()
        {
            return _entities;
        }
    }
}
