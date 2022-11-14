using FlapKap.Application.Models;

namespace FlapKap.Application.Interfaces
{
    public interface IProductService:IBaseCRUDService<ProductModel>
    {
        Task<BoughtProductModel> Buy(int productId, int quantity);
    }
}
