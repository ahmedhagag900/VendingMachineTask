using FluentValidation;
using System.Reflection.Metadata;

namespace FlapKap.API.APIRequests.User
{
    public class DepositAPIRequst
    {
        public int Amount { get; set; }
    }
    public class DepositAPIRequestValidator:AbstractValidator<DepositAPIRequst>
    {
        public DepositAPIRequestValidator(Core.Constatnt.Constants constants) 
        {
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            
            RuleFor(x => x.Amount)
                .Must(x => constants.ChangeCoins.Contains(x))
                .WithMessage("amount must be in the following range [5,10,20,50,100]");
        }

    }
}
