using FluentValidation;

namespace FlapKap.API.APIRequests.User
{
    public class LoginAPIRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginAPIRequestValidator:AbstractValidator<LoginAPIRequest>
    {
        public LoginAPIRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
