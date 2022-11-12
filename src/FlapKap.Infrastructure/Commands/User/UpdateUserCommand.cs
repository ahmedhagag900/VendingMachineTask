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
    public class UpdateUserCommand:IRequest<UserModel>
    {
        public UpdateUserCommand(UserModel user)
        {
            User = user;    
        }
        public UserModel User { get; } 
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserModel>
    {
        private readonly IUserService _userService;
        public UpdateUserCommandHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task<UserModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.Update(request.User, cancellationToken);
            return result;
        }
    }

}
