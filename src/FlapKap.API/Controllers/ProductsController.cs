using FlapKap.Application.Models;
using FlapKap.Infrastructure.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlapKap.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IMediator mediator,IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var result = await _mediator.Send(new GetAllUsersRequest(), cancellationToken);
            //return Ok(result);
        }


        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProductById([FromRoute] int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var result = await _mediator.Send(new GetUserByIdRequest(userId), cancellationToken);
            //return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct(ProductModel product, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //var result = await _mediator.Send(new CreateUserCommand(user), cancellationToken);


            //var serverUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            //var createdResource = Path.Combine(serverUrl, result.Id.ToString());

            //return Created(new Uri(createdResource), result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct(ProductModel user, CancellationToken cancellationToken)
        {
            throw new  NotImplementedException();
            //var result = await _mediator.Send(new UpdateUserCommand(user), cancellationToken);
            //return Ok(result);
        }

        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute] int productId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
            //return NoContent();
        }

    }
}
