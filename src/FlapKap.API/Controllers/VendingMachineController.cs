using VendingMachine.API.APIRequests.Product;
using VendingMachine.API.APIRequests.User;
using VendingMachine.API.Constants;
using VendingMachine.Application.Models;
using VendingMachine.Infrastructure.Commands.Product;
using VendingMachine.Infrastructure.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace VendingMachine.API.Controllers
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
