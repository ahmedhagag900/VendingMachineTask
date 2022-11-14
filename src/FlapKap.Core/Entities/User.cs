using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Core.Entities
{
    public partial class User
    {
        public User()
        {
            SellerProducts=new HashSet<Product>();
            BuyerProducts = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public double Deposit { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Product> SellerProducts { get; set; }
        public virtual ICollection<Product> BuyerProducts { get; set; }
    }
}
