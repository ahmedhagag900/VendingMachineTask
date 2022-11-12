using FlapKap.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure.Commands.User
{
    public class DeleteUserCommand:IRequest
    {
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; }
    }

    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.Delete(request.UserId);
            return new Unit();
        }
    }

}
