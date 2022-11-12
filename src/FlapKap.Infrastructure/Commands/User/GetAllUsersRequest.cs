using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using FlapKap.Core.Repositories;
using MediatR;

namespace FlapKap.Infrastructure.Commands.User
{
    public class GetAllUsersRequest : IRequest<IEnumerable<UserModel>>
    {
    }

    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<UserModel>>
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
