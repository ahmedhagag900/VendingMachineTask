using VendingMachine.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Interfaces
{
    internal interface IBusinessRule
    {
        public ApplicationCode ErrorCode { get; }
        public string Message { get; }
        Task<bool> IsBroken();
    }
}
