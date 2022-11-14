using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Core.Entities
{
    public partial class Product
    {
        public Product()
        {
            Buyers = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }   
        public int AvailableAmount { get; set; }
        public int SellerId { get; set; }
        public virtual User Seller { get; set; }
        public virtual ICollection<User> Buyers { get; set; }
    }
}
