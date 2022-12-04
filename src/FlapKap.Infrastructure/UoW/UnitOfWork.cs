using VendingMachine.Core.Repositories;
using VendingMachine.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.UoW
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly VendingMachieneContext _context;
        public UnitOfWork(VendingMachieneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
