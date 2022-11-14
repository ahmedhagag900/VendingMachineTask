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
        private readonly IUnitOfWork _unitOfWork;
        public ContextSeed(IRoleRepository roleRepository,IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedRoleAsync()
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
    }
}
