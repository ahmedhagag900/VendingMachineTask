using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Commands.User
{
    public class LoginCommand:IRequest<LoginModel>
    {
        public LoginCommand(string userName,string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; }
        public string Password { get;  }
    }

    internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginModel>
    {
        private readonly IUserService _userService;
        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task<LoginModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(request.UserName, request.Password);
            return result;
        }
    }
}
