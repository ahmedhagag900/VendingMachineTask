using VendingMachine.Core.Entities;
using VendingMachine.Core.Repositories;

namespace VendingMachine.Infrastructure.Repositories
{
    internal class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(VendingMachieneContext context):base(context)
        {

        }
    }
}
