using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Core
{
    public interface IExecutionContext
    {
        public int UserId { get; }
    }
}
