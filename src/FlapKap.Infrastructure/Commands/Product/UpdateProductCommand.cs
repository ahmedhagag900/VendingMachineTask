using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using MediatR;

namespace FlapKap.Infrastructure.Commands.Product
{
    public class UpdateProductCommand:IRequest<ProductModel>
    {
        public UpdateProductCommand(ProductModel product)
        {
            Product = product;    
        }
        public ProductModel Product { get; } 
    }

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateProductCommand, ProductModel>
    {
        private readonly IProductService _productService;
        public UpdateUserCommandHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<ProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.Update(request.Product, cancellationToken);
            return result;
        }
    }

}
