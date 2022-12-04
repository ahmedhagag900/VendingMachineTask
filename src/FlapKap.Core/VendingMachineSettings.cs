using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core
{
    public class VendingMachineSettings
    {
        public string ConnectionString { get; set; }
        public JWTOptions JWTOptions { get; set; }
    }

    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
