using FluentValidation;

namespace FlapKap.API.APIRequests.Product
{
    public class BuyProductAPIRequest
    {
        public int ProductId { get; set; }  
        public int Quantity { get; set; }
    }

    public class BuyProductAPIRequestValidator: AbstractValidator<BuyProductAPIRequest>
    {
        public BuyProductAPIRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }

}
