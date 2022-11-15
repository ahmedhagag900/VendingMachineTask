namespace FlapKap.Core.Entities
{
    public class BuyHistory
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }    
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
