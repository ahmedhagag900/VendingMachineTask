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
    public class GetUserByIdRequest:QueryBase,IRequest<UserModel>
    {
        public GetUserByIdRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }

    internal class GetUserByIdRequstHandler : IRequestHandler<GetUserByIdRequest, UserModel>
    {
        private readonly IUserService _userService;

        public GetUserByIdRequstHandler(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<UserModel> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetById(request.UserId);
            return result;
        }
    }

}
