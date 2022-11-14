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
    internal class CanBuyProductRule:IBusinessRule
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly int _productId;
        private readonly int _quantity;
        private readonly int _userId;
        public CanBuyProductRule(int productId,int quantity,int userId, IProductRepository productRepository,IUserRepository userRepository)
        {
            _productId = productId;
            _quantity = quantity;
            _userId = userId;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public ApplicationCode ErrorCode => ApplicationCode.ProductDoseNotExisits;

        public string Message => "Remaining balanc is not enough to buy the product with requested quantity";

        public async Task<bool> IsBroken()
        {
            var product = await _productRepository.GetByIdAsync(_productId);
            var user = await _userRepository.GetByIdAsync(_userId);

            var totalPriceTobuy = product.Price * _quantity;

            return user.Deposit < totalPriceTobuy;
        }
    }
}
