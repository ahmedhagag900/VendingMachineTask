using FlapKap.API.APIRequests.User;
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
    [Route("api/[controller]")]
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
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsersRequest(),cancellationToken);
            return Ok(result);
        }


        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserById([FromRoute] int userId, CancellationToken cancellationToken)
        {
            var result =await _mediator.Send(new GetUserByIdRequest(userId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserModel user,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserCommand(user),cancellationToken);

            
            var serverUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var createdResource = Path.Combine(serverUrl, result.Id.ToString());

            return Created(new Uri(createdResource), result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser(UserModel user, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateUserCommand(user), cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute]int userId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
            return NoContent();
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(LoginModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody]LoginAPIRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LoginCommand(request.UserName, request.Password),cancellationToken);
            return Ok(result);
        }

    }
}
