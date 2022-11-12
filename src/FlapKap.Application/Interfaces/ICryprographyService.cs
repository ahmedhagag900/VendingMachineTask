using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Application.Interfaces
{
    internal interface ICryprographyService
    {
        Task<string> HashAsync(string seed);
    }
}
