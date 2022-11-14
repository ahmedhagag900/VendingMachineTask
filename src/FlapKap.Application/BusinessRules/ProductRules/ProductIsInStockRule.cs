using FlapKap.Application.Interfaces;
using FlapKap.Core.Enums;
using FlapKap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.BusinessRules.ProductRules
{
    internal class ProductIsInStockRule:IBusinessRule
    {
        private readonly IProductRepository _productRepository;
        private readonly int _quantity;
        private readonly int _productId;
        public ProductIsInStockRule(int productId,int quantity, IProductRepository productRepository)
        {
            _quantity = quantity;
            _productId = productId;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public ApplicationCode ErrorCode => ApplicationCode.ProductOutOfStock;

        public string Message => "Product is out of stock";

        public async Task<bool> IsBroken()
        {
            var product = await _productRepository.GetByIdAsync(_productId);

            return product.AvailableAmount < _quantity;
        }
    }
}
