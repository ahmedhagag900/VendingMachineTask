using System.Linq.Expressions;

namespace FlapKap.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetData(Expression<Func<T,bool>>? predicate=null);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T,bool>>? predicate = null);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> Query();
    }
}
