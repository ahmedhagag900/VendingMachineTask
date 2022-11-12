using FlapKap.Core.Repositories;
using FlapKap.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly VendingMachieneContext _context;
        public UnitOfWork(VendingMachieneContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
