using FlapKap.Core.Repositories;

namespace FlapKap.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();

    }
}
