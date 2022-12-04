using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Interfaces
{
    internal interface ICryprographyService
    {
        Task<string> HashAsync(string seed);
    }
}
