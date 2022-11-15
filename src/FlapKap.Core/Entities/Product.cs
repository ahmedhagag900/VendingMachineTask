namespace FlapKap.Core.Entities
{
    public partial class Product
    {
        public Product()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }   
        public int AvailableAmount { get; set; }
        public int SellerId { get; set; }
        public virtual User Seller { get; set; }
    }
}
