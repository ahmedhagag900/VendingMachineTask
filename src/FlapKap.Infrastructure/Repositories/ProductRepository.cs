using FlapKap.Core.Entities;
using FlapKap.Core.Repositories;

namespace FlapKap.Infrastructure.Repositories
{
    internal class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(VendingMachieneContext context):base(context)
        {

        }
    }
}
