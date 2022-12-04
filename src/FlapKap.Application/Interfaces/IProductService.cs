using VendingMachine.Application.Models;

namespace VendingMachine.Application.Interfaces
{
    public interface IProductService:IBaseCRUDService<ProductModel>
    {
        Task<BoughtProductModel> Buy(int productId, int quantity);
    }
}
