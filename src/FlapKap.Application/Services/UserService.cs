using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
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
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryprographyService _cryprographyService;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository,
            ICryprographyService cryprographyService,
            IUnitOfWork unitOfWork)
        {
            _cryprographyService = cryprographyService ?? throw new ArgumentNullException(nameof(cryprographyService));
            _userRepository=userRepository?? throw new ArgumentNullException(nameof(userRepository));  
            _unitOfWork=unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<UserModel> Add(UserModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Password = await _cryprographyService.HashAsync(model.Password),
                Name = model.Name,
                RoleId = model.RoleId
            };

            var added=_userRepository.Add(user);
            await _unitOfWork.CompleteAsync();
            return new UserModel
            {
                Id = added.Id,
                UserName = added.UserName,
                Name = added.Name,
                RoleId=added.RoleId
            };
        }

        public async Task Delete(int id)
        {
            var userToDelete = await _userRepository.GetByIdAsync(id);
            if(userToDelete!=null)
                _userRepository.Delete(userToDelete);
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return (await _userRepository.GetAsync(c => true)).Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
                RoleId = u.RoleId,
                UserName = u.UserName
            });
        }

        public async Task<UserModel> GetById(int id)
        {
            var user= await _userRepository.GetByIdAsync(id);
            if (user == null)
                return new UserModel();

            return new UserModel
            {
                UserName = user.UserName,
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId
            };
        }

        public async Task<UserModel> Update(UserModel model)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(model.Id);
            if (userToUpdate == null)
                return new UserModel();


            //role to check user name and role id;

            userToUpdate.UserName = model.UserName;
            userToUpdate.RoleId = model.RoleId;
            userToUpdate.Name = model.Name;
            userToUpdate.Password = await _cryprographyService.HashAsync(model.Password);

            var updated=_userRepository.Update(userToUpdate);
            await _unitOfWork.CompleteAsync();
            return new UserModel
            {
                Id = updated.Id,
                Name = updated.Name,
                UserName = updated.UserName,
                RoleId = updated.RoleId
            };
        }
    }
}
