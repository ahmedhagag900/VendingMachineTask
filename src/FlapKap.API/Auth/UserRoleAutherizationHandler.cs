using FlapKap.Core.Enums;
using FlapKap.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlapKap.API.Auth
{
    public class UserRoleRequirement:IAuthorizationRequirement
    {
        public UserRoleRequirement(int roleId)
        {
            RoleId = roleId;
        }
        public int RoleId { get; }
    }
    public class UserRoleAutherizationHandler : AuthorizationHandler<UserRoleRequirement>
    {

        private readonly IUserRepository _userRepository;
        public UserRoleAutherizationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
        {
            
            var userIdString = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null)
                context.Fail();
            
            int.TryParse(userIdString,out int userId);

            if (userId < 0)
                context.Fail();

            var user = await _userRepository.GetByIdAsync(userId);

            if (user?.RoleId != requirement.RoleId)
                context.Fail();

            context.Succeed(requirement);

        }
    }
}
