﻿using VendingMachine.API.APIRequests.Product;
using VendingMachine.API.Constants;
using VendingMachine.Application.Models;
using VendingMachine.Infrastructure.Commands.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace VendingMachine.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(policy:Policy.Seller)]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductsRequest(), cancellationToken);
            return Ok(result);
        }


        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById([FromRoute] int productId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdRequest(productId), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct(ProductAPIRequest product, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateProductCommand(new ProductModel
            {
                Name = product.Name,
                Price = product.Price,
                AvailableAmount=product.AvailableAmount
            }), cancellationToken);


            var serverUrl = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var createdResource = Path.Combine(serverUrl, result.Id.ToString());

            return Created(new Uri(createdResource), result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct(ProductAPIRequest product, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateProductCommand(new ProductModel
            {
                Id=product.ProductId,
                AvailableAmount = product.AvailableAmount,
                Name=product.Name,
                Price=product.Price,
            }), cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute] int productId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProductCommand(productId), cancellationToken);
            return NoContent();
        }

    }
}
