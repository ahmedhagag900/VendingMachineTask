using VendingMachine.Application.Models;

namespace VendingMachine.Application.Interfaces
{
    public interface IBaseCRUDService<T> where T : BaseModel
    {
        Task<T> Add(T model,CancellationToken cancellationToken);
        Task<T> Update(T model, CancellationToken cancellationToken);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
