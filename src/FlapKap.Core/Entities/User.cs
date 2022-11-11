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
            Products=new HashSet<Product>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
