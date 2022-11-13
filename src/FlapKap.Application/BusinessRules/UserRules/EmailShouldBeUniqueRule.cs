using FlapKap.Application.Interfaces;
using FlapKap.Core.Enums;
using FlapKap.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.BusinessRules.UserRules
{
    internal class UserNameShouldBeUniqueRule : IBusinessRule
    {
        private readonly IUserRepository _userRepository;
        private readonly string _userName;
        public UserNameShouldBeUniqueRule(string userName,IUserRepository userRepository)
        {
            _userName = userName;
            _userRepository = userRepository??throw new ArgumentNullException(nameof(userRepository));
        }
        public ErrorCode ErrorCode => ErrorCode.UserNameExists;

        public string Message => $"This email{_userName} already exists";

        public async Task<bool> IsBroken()
        {
            var user = (await _userRepository.GetAsync(u => u.UserName == _userName)).FirstOrDefault();
            return user != null;
        }
    }
}
