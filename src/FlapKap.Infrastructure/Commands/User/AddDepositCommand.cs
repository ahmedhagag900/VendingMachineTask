using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Models;
using MediatR;

namespace VendingMachine.Infrastructure.Commands.User
{
    public class AddDepositCommand:IRequest<DepositModel>
    {
        public AddDepositCommand(double amount)
        {
            Amount = amount;    
        }
        public double Amount { get; }
    }

    internal class AddDepositCommandHandler : IRequestHandler<AddDepositCommand, DepositModel>
    {
        private readonly IUserService _userService;
        public AddDepositCommandHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public  async Task<DepositModel> Handle(AddDepositCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddDeposit(request.Amount);
            return result;
        }
    }
}
