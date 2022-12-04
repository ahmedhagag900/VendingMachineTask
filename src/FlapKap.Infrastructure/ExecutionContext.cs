using VendingMachine.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure
{
    internal class ExecutionContext : IExecutionContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExecutionContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public int UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                return Convert.ToInt32(userId ?? "0");
            }
        }
    }
}
