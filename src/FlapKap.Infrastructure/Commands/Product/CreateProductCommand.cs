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
    public class CreateProductCommand:IRequest<ProductModel>
    {
        public CreateProductCommand(ProductModel product)
        {
            Product = product;
        }

        public ProductModel Product { get;}
    }

    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductModel>
    {
        private readonly IProductService _productService;
        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<ProductModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.Add(request.Product,cancellationToken);
            return result;
        }
    }

}
