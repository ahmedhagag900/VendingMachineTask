using VendingMachine.Application.Interfaces;
using VendingMachine.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.IoC
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<ICryprographyService, CryprographyService>();
            

            return service;
        }
    }
}
