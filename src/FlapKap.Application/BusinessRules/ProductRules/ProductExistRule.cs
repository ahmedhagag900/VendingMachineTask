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
    internal class ProductExistRule : IBusinessRule
    {
        private readonly IProductRepository _productRepository;
        private readonly int _productId;
        public ProductExistRule(int productId,IProductRepository productRepository)
        {
            _productId = productId;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)); 
        }
        public ApplicationCode ErrorCode => ApplicationCode.ProductDoseNotExisits;

        public string Message => "Product not found";

        public async Task<bool> IsBroken()
        {
            var product=await _productRepository.GetByIdAsync(_productId);
            return product == null;
        }
    }
}
