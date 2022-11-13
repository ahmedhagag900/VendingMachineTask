using FlapKap.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.Models
{
    public class UserModel: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public double Deposit { get; set; }
        public UserRole RoleId { get; set; }
    }
}
