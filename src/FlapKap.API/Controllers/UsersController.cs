using FlapKap.API.APIRequests.User;
using FlapKap.API.Constants;
using FlapKap.Application.Models;
using FlapKap.Infrastructure.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlapKap.API.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class UsersController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IMediator mediator,IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [HttpGet]
        [Route("[controller]")]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsersRequest(),cancellationToken);
            return Ok(result);
        }


        [HttpGet]
        [Route("[controller]/{userId}")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserById([FromRoute] int userId, CancellationToken cancellationToken)
        {
            var result =await _mediator.Send(new GetUserByIdRequest(userId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("[controller]")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserAPIRequest user,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserCommand(new UserModel
            {
                UserName = user.UserName,
                Name = user.Name,
                Password = user.Password,
                RoleId = user.RoleId
            }),cancellationToken);

            
            var serverUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var createdResource = Path.Combine(serverUrl, result.Id.ToString());

            return Created(new Uri(createdResource), result);
        }

        [HttpPut]
        [Route("[controller]")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser(UserAPIRequest user, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateUserCommand(new UserModel
            {
                Id = user.UserId,
                RoleId = user.RoleId,
                Password = user.Password,
                Name = user.Name,
                UserName = user.UserName
            }), cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [Route("[controller]/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute]int userId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
            return NoContent();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [ProducesResponseType(typeof(LoginModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginAPIRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LoginCommand(request.UserName, request.Password),cancellationToken);
            return Ok(result);
        }

        
        

    }
}
