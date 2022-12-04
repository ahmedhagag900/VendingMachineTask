using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Commands.Product
{
    public class GetProductByIdRequest:QueryBase,IRequest<ProductModel>
    {
        public GetProductByIdRequest(int userId)
        {
            ProductId = userId;
        }

        public int ProductId { get; }
    }

    internal class GetUserByIdRequstHandler : IRequestHandler<GetProductByIdRequest, ProductModel>
    {
        private readonly IProductService _productService;

        public GetUserByIdRequstHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        public async Task<ProductModel> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _productService.GetById(request.ProductId);
            return result;
        }
    }

}
