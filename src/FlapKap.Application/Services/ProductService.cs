﻿using VendingMachine.Application.BusinessRules.ProductRules;
using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Models;
using VendingMachine.Core;
using VendingMachine.Core.Constatnt;
using VendingMachine.Core.Entities;
using VendingMachine.Core.Repositories;
using VendingMachine.Core.UnitOfWork;
using System.Linq.Expressions;

namespace VendingMachine.Application.Services
{
    internal class ProductService :ServiceBase,  IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExecutionContext _executionContext;
        private readonly IBaseRepository<BuyHistory> _buyHistoryRepository;
        private readonly Constants _constants;
        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IExecutionContext executionContext,
            IBaseRepository<BuyHistory> buyHistoryRepository,
            Constants constants)
        {
            _buyHistoryRepository = buyHistoryRepository??throw new ArgumentNullException(nameof(buyHistoryRepository));
            _constants=constants?? throw new ArgumentNullException(nameof(constants));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));    
            _executionContext = executionContext ?? throw new ArgumentNullException(nameof(executionContext));
        }
        public async Task<ProductModel> Add(ProductModel model, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = model.Name,
                AvailableAmount = model.AvailableAmount,
                Price = model.Price,
                SellerId = _executionContext.UserId
            };
            var added=await _productRepository.AddAsync(product,cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return new ProductModel
            {
                Id = added.Id,
                AvailableAmount = added.AvailableAmount,
                Name = added.Name,
                Price = added.Price,
                SellerId = _executionContext.UserId
            };

        }

        public async Task<BoughtProductModel> Buy(int productId, int quantity)
        {
            await CheckRule(new ProductExistRule(productId, _productRepository));
            await CheckRule(new ProductIsInStockRule(productId,quantity, _productRepository));
            await CheckRule(new CanBuyProductRule(productId, quantity, _executionContext.UserId, _productRepository, _userRepository));


            var user = await _userRepository.GetByIdAsync(_executionContext.UserId);
            var product = await _productRepository.GetByIdAsync(productId);
            
            var totalPrice = product.Price * quantity;

            product.AvailableAmount -= quantity;
            var changeToCalculate=user.Deposit-totalPrice;
            user.Deposit = 0;

            var history = new BuyHistory
            {
                ProductId = product.Id,
                UserId = user.Id,
                Name = product.Name,
                Quantity = quantity,
                BuyDate = DateTime.Now,
                TotalCost = totalPrice
            };
            
            await _buyHistoryRepository.AddAsync(history, new CancellationToken());
            _userRepository.Update(user);
            _productRepository.Update(product);

            return new BoughtProductModel
            {
                TotalCost = totalPrice,
                ProductBoughtCount=quantity,
                Product=new ProductModel
                {
                    Id=product.Id,
                    AvailableAmount=product.AvailableAmount,
                    Name=product.Name,
                    Price = product.Price,
                    SellerId = product.SellerId,
                },
                Change=new ChangeCoinModel
                {
                    Coins=CalculateChange(changeToCalculate,_constants.ChangeCoins)
                }
            };

        }

        private Dictionary<int,long> CalculateChange(double amount, int[] coins)
        {
            Array.Sort(coins);
            Dictionary<int, long> ans = new Dictionary<int, long>();
            for (int i = coins.Length-1;i>=0 ;--i)
            {
                if(amount>=coins[i])
                {
                    long val=((long)amount/coins[i]);
                    amount -= (val * coins[i]);
                    ans.Add(coins[i], val);
                }
            }
            return ans;
        }

        public async Task Delete(int id)
        {
            await CheckRule(new ProductExistRule(id, _productRepository));

            var productToDelete = await _productRepository.GetByIdAsync(id);
            _productRepository.Delete(productToDelete);
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            return (await _productRepository.GetAsync(c => true)).Select(p => new ProductModel
            {
                Id = p.Id,
                AvailableAmount = p.AvailableAmount,
                Name = p.Name,
                Price = p.Price,
                SellerId = p.SellerId
            });
        }

        public async Task<ProductModel> GetById(int id)
        {

            await CheckRule(new ProductExistRule(id, _productRepository));

            var product = await _productRepository.GetByIdAsync(id);
            
            return new ProductModel
            {
                Id = product.Id,
                AvailableAmount = product.AvailableAmount,
                Name = product.Name,
                Price = product.Price,
                SellerId = product.SellerId
            };

        }

        public async  Task<ProductModel> Update(ProductModel model, CancellationToken cancellationToken)
        {

            await CheckRule(new ProductExistRule(model.Id, _productRepository));

            //add includes

            Expression<Func<Product, object>> sellerInclude = p => p.Seller;

            var productToUpdate = (await _productRepository.GetAsync(p => p.Id == model.Id, new List<Expression<Func<Product, object>>> { sellerInclude }, cancellationToken)).SingleOrDefault();
            

            productToUpdate.Price = model.Price;
            productToUpdate.Name = model.Name;
            productToUpdate.AvailableAmount = model.AvailableAmount;

            var updated=_productRepository.Update(productToUpdate);
            await _unitOfWork.CompleteAsync(cancellationToken);

            return new ProductModel
            {
                Id = updated.Id,
                AvailableAmount = updated.AvailableAmount,
                Name = updated.Name,
                Price = updated.Price,
                SellerId = updated.SellerId
            };

        }
    }
}
