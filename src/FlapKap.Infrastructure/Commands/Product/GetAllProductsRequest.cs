using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using MediatR;

namespace FlapKap.Infrastructure.Commands.Product
{
    public class GetAllProductsRequest :QueryBase, IRequest<IEnumerable<ProductModel>>
    {
    }

    internal class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<ProductModel>>
    {
        private readonly IProductService _productService;
        public GetAllProductsRequestHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IEnumerable<ProductModel>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAll();

            return result;
        }
    }
}
