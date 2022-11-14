namespace FlapKap.API.APIRequests.Product
{
    public class ProductAPIRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; } 
        public int AvailableAmount { get; set; }
    }
}
