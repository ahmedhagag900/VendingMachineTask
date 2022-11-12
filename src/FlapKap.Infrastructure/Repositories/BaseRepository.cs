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

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>>? predicate = null,List<Expression<Func<T,object>>>? includes=null,CancellationToken cancellationToken=default)
        {
            var query = _entities.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if(includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }    

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, List<Expression<Func<T, object>>>? includes = null, CancellationToken cancellationToken = default)
        {
            var query = _entities.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity,CancellationToken cancellationToken)
        {
            await _entities.AddAsync(entity,cancellationToken);
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
