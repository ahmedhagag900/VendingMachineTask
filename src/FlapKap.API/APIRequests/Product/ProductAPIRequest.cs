using FluentValidation;

namespace VendingMachine.API.APIRequests.Product
{
    public class ProductAPIRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; } 
        public int AvailableAmount { get; set; }
    }
    public class ProductAPIRequestValidator:AbstractValidator<ProductAPIRequest>
    {
        public ProductAPIRequestValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.AvailableAmount).GreaterThan(0);
            RuleFor(x => x.Price).Must(x => x % 5 == 0).WithMessage("price must be multiple of 5");
        }
    }
}
