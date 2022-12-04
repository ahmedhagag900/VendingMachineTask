using VendingMachine.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Exceptions
{
    public class BusinessRuleException:Exception
    {
        public ApplicationCode ErrorCode { get; }
        public BusinessRuleException(string message,ApplicationCode error):base(message)
        {
            ErrorCode = error;
        }
    }
}
