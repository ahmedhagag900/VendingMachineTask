using VendingMachine.Application.Exceptions;
using VendingMachine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services
{
    internal abstract class ServiceBase
    {
        protected virtual async Task CheckRule(IBusinessRule businessRule)
        {
            if(await businessRule.IsBroken())
            {
                throw new BusinessRuleException(businessRule.Message,businessRule.ErrorCode);
            }
        }

    }
}
