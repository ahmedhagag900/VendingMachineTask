using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using FlapKap.Core;
using FlapKap.Core.Entities;
using FlapKap.Core.Repositories;
using FlapKap.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExecutionContext _executionContext;
        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IExecutionContext executionContext)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));    
            _executionContext = executionContext ?? throw new ArgumentNullException(nameof(executionContext));
        }
        public async Task<ProductModel> Add(ProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                AvailableAmount = model.AvailableAmount,
                Price = model.Price,
                SellerId = _executionContext.UserId
            };
            var added=_productRepository.Add(product);
            await _unitOfWork.CompleteAsync();

            return new ProductModel
            {
                Id = added.Id,
                AvailableAmount = added.AvailableAmount,
                Name = added.Name,
                Price = added.Price,
                SellerId = _executionContext.UserId,
                SellerName = added.Seller.Name
            };

        }

        public async Task Delete(int id)
        {
            var productToDelete = await _productRepository.GetByIdAsync(id);
            if (productToDelete != null)
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
                SellerId = p.SellerId,
                SellerName = p.Seller.Name
            });
        }

        public async Task<ProductModel> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return new ProductModel();

            return new ProductModel
            {
                Id = product.Id,
                AvailableAmount = product.AvailableAmount,
                Name = product.Name,
                Price = product.Price,
                SellerId = product.SellerId,
                SellerName = product.Seller.Name
            };

        }

        public async  Task<ProductModel> Update(ProductModel model)
        {
            //add includes
            var productToUpdate = await _productRepository.GetByIdAsync(model.Id);
            if (productToUpdate == null)
                return new ProductModel();

            productToUpdate.Price = model.Price;
            productToUpdate.Name = model.Name;
            productToUpdate.AvailableAmount = model.AvailableAmount;

            var updated=_productRepository.Update(productToUpdate);
            await _unitOfWork.CompleteAsync();

            return new ProductModel
            {
                Id = updated.Id,
                AvailableAmount = updated.AvailableAmount,
                Name = updated.Name,
                Price = updated.Price,
                SellerId = updated.SellerId,
                SellerName = updated.Seller.Name
            };

        }
    }
}
