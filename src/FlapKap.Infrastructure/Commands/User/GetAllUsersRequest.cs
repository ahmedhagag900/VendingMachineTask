using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Models;
using VendingMachine.Core.Repositories;
using MediatR;

namespace VendingMachine.Infrastructure.Commands.User
{
    public class GetAllUsersRequest :QueryBase, IRequest<IEnumerable<UserModel>>
    {
    }

    internal class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<UserModel>>
    {
        private readonly IUserService _userService;
        public GetAllUsersRequestHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IEnumerable<UserModel>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetAll();

            return result;
        }
    }
}
