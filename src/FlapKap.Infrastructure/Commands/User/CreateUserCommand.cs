using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure.Commands.User
{
    public class CreateUserCommand:IRequest<UserModel>
    {
        public CreateUserCommand(UserModel user)
        {
            User = user;
        }

        public UserModel User { get;}
    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserModel>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task<UserModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.Add(request.User,cancellationToken);
            return result;
        }
    }

}
