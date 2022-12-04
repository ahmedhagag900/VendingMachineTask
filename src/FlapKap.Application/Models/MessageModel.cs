using VendingMachine.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Models
{
    public class MessageModel
    {
        public ApplicationCode Code { get; set; }
        public string Message { get; set; }
    }
}
