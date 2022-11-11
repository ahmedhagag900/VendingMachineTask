using System.Linq.Expressions;

namespace FlapKap.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> Get(Expression<Predicate<T>> predicate);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync(Expression<Predicate<T>>? predicate = null, Expression<Action<T>>? orderBy = null, int? skip = null, int? take = null);

    }
}
