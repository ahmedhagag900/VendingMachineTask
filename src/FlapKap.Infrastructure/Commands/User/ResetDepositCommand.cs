using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Models;
using MediatR;

namespace VendingMachine.Infrastructure.Commands.User
{
    public class ResetDepositCommand:IRequest<DepositModel>
    {
    }
    internal class ResetDepositCommandHandler : IRequestHandler<ResetDepositCommand, DepositModel>
    {
        private readonly IUserService _userService;
        public ResetDepositCommandHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task<DepositModel> Handle(ResetDepositCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.ResetDeposit();
            return result;
        }
    }
}
