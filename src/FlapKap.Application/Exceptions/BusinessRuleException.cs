using FlapKap.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.Exceptions
{
    public class BusinessRuleException:Exception
    {
        public ErrorCode ErrorCode { get; }
        public BusinessRuleException(string message,ErrorCode error):base(message)
        {
            ErrorCode = error;
        }
    }
}
