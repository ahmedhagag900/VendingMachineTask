using VendingMachine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Commands.Product
{
    public class DeleteProductCommand:IRequest
    {
        public DeleteProductCommand(int productId)
        {
            ProuductId = productId;
        }
        public int ProuductId { get; }
    }

    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService??throw new ArgumentNullException(nameof(productService));
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productService.Delete(request.ProuductId);
            return new Unit();
        }
    }

}
