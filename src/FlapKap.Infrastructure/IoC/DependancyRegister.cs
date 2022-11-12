﻿using FlapKap.Core.Repositories;
using FlapKap.Core.UnitOfWork;
using FlapKap.Infrastructure.PipeLines;
using FlapKap.Infrastructure.Repositories;
using FlapKap.Infrastructure.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlapKap.Infrastructure.IoC
{
    public static class DependancyRegister
    {
        public static IServiceCollection RegisterSqlServerDbContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<VendingMachieneContext>(opt =>
            {
                opt.UseSqlServer(connectionString);

            }, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection RegisterInfraStructureServices(IServiceCollection services)
        {

            services.AddMediatR(typeof(DependancyRegister).Assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipeLine<,>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


        /// <summary>
        /// used for testing and if you dont need to install sql server db
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterInMemmoryDBContext(this IServiceCollection services)
        {
            services.AddDbContext<VendingMachieneContext>(opt =>
            {
                opt.UseInMemoryDatabase("VendingMacheine");
            });
            return services;
        }


    }
}
