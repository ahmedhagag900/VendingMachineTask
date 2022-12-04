using System.Linq.Expressions;

namespace VendingMachine.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken=default);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>>? predicate = null,List<Expression<Func<T,object>>>? includes=null,CancellationToken cancellationToken=default);
        Task<T> AddAsync(T entity,CancellationToken cancellationToken);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> Query();
    }
}
