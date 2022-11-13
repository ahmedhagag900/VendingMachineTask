using FlapKap.Application.Exceptions;
using FlapKap.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.Services
{
    internal abstract class ServiceBase
    {
        protected virtual async Task CheckRule(IBusinessRule businessRule)
        {
            if(await businessRule.IsBroken())
            {
                throw new BusinessRuleException(businessRule.Message, businessRule.ErrorCode);
            }
        }

    }
}
