using FlapKap.Application.Models;
using FlapKap.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlapKap.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersRequest());
            return Ok(result);
        }

    }
}
