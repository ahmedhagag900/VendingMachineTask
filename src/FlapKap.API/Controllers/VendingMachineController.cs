using FlapKap.API.APIRequests.Product;
using FlapKap.API.APIRequests.User;
using FlapKap.API.Constants;
using FlapKap.Application.Models;
using FlapKap.Infrastructure.Commands.Product;
using FlapKap.Infrastructure.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlapKap.API.Controllers
{
    [ApiController]
    public class VendingMachineController:ControllerBase
    {
        private readonly IMediator _mediator;

        public VendingMachineController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut]
        [Route("deposit")]
        [Authorize(policy: Policy.Buyer)]
        [ProducesResponseType(typeof(DepositModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddDeposit([FromBody] DepositAPIRequst request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddDepositCommand(request.Amount), cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        [Route("reset")]
        [Authorize(policy: Policy.Buyer)]
        [ProducesResponseType(typeof(DepositModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetDeposit(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ResetDepositCommand(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("buy")]
        [Authorize(policy: Policy.Buyer)]
        [ProducesResponseType(typeof(BoughtProductModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MessageModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BuyProduct([FromBody]BuyProductAPIRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new BuyProductCommand(request.ProductId,request.Quantity), cancellationToken);
            return Ok(result);
        }


    }
}
