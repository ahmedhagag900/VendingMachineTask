using VendingMachine.Core.Repositories;

namespace VendingMachine.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync(CancellationToken cancellationToken);

    }
}
