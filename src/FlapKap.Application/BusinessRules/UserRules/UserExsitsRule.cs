using FlapKap.Application.Interfaces;
using FlapKap.Core.Enums;
using FlapKap.Core.Repositories;

namespace FlapKap.Application.BusinessRules.UserRules
{
    internal class UserExsitsRule : IBusinessRule
    {
        private readonly int _userId;
        private readonly string _userName;
        private readonly IUserRepository _userRepository;
        public UserExsitsRule(int userId,IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userId = userId;
        }
        public UserExsitsRule(string userName, IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userName = userName;
        }
        public ApplicationCode ErrorCode => ApplicationCode.UserDoesNotExists;

        public string Message => "User not found";

        public async Task<bool> IsBroken()
        {
            var userById = await  _userRepository.GetByIdAsync(_userId);
            var userByUserName = (await _userRepository.GetAsync(u => u.UserName == _userName)).SingleOrDefault();
            return userById == null && userByUserName == null;
        }
    }
}
