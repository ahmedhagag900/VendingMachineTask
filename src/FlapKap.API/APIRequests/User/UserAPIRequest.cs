using VendingMachine.Core.Enums;
using FluentValidation;

namespace VendingMachine.API.APIRequests.User
{
    public class UserAPIRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole RoleId { get; set; }
        
    }

    public class UserAPIRequestValidator:AbstractValidator<UserAPIRequest>
    {
        public UserAPIRequestValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }

}
