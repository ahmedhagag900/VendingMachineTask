using FlapKap.Core.Entities;
using FlapKap.Core.Enums;
using FlapKap.Core.Repositories;
using FlapKap.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure
{
    public class ContextSeed
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ContextSeed(IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task SeedRolesAsync()
        {
            CancellationToken cancellation = new CancellationToken();
            await _roleRepository.AddAsync(new Core.Entities.Role
            {
                Id = (int)UserRole.Buyer,
                Name = "Buyer"
            },cancellation);
            await _roleRepository.AddAsync(new Core.Entities.Role
            {
                Id = (int)UserRole.Seller,
                Name = "Seller"
            }, cancellation);
            await _roleRepository.AddAsync(new Core.Entities.Role
            {
                Id = (int)UserRole.SA,
                Name = "Super Admin"
            }, cancellation);

            await _unitOfWork.CompleteAsync(cancellation);
        }

        public async Task SeedUsersAsync()
        {
            CancellationToken cancellation = new CancellationToken();
            await _userRepository.AddAsync(new User
            {
                Name = "Ahmed hagag",
                UserName = "seller",
                Password= "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                RoleId = (int)UserRole.Seller,
            },cancellation);

            await _userRepository.AddAsync(new User
            {
                Name = "mohamed hagag",
                UserName = "buyer",
                RoleId = (int)UserRole.Buyer,
                Password= "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92"
            }, cancellation);

            await _unitOfWork.CompleteAsync(cancellation);
        }

        public async Task SeedProductsAsync()
        {
            CancellationToken cancellation = new CancellationToken();
            await _productRepository.AddAsync(new Product
            {
                Name = "product 1",
                Price = 50,
                AvailableAmount = 4,
                SellerId = 1
            }, cancellation);

            await _productRepository.AddAsync(new Product
            {
                Name = "product 2",
                Price = 60,
                AvailableAmount = 7,
                SellerId = 1
            }, cancellation);
            await _productRepository.AddAsync(new Product
            {
                Name = "product 3",
                Price = 10,
                AvailableAmount = 6,
                SellerId = 1
            }, cancellation);


            await _unitOfWork.CompleteAsync(cancellation);
        }

    }
}
