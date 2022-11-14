using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure.Commands.Product
{
    public class BuyProductCommand:IRequest<BoughtProductModel>
    {
        public BuyProductCommand(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    
        public int ProductId { get; }
        public int Quantity { get; }
    }

    internal class BuyProductCommandHandler : IRequestHandler<BuyProductCommand, BoughtProductModel>
    {
        private readonly IProductService _productService;

        public BuyProductCommandHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<BoughtProductModel> Handle(BuyProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.Buy(request.ProductId, request.Quantity);

            return result;
        }
    }

}
