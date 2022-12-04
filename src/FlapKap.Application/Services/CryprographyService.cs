using VendingMachine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services
{
    internal class CryprographyService : ICryprographyService
    {
        public Task<string> HashAsync(string seed)
        {
            using(var hasher=SHA256.Create())
            {
                var hashedByte=hasher.ComputeHash(Encoding.UTF8.GetBytes(seed));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedByte.Length; i++)
                {
                    builder.Append(hashedByte[i].ToString("x2"));
                }
                return Task.FromResult(builder.ToString());
            }
        }
    }
}
