using FlapKap.Core;
using FlapKap.Core.Repositories;
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

        public static IServiceCollection RegisterInfraStructureServices(this IServiceCollection services,bool inMemotyDb=true)
        {

            services.AddMediatR(typeof(DependancyRegister).Assembly);

            // if set to ture then regester the in memory data base pipeLine 
            if (inMemotyDb)
            {
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MemoryDBPipeLine<,>));
            }
            else //regeister the transaction pipe line 
            {
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipeLine<,>));
            }
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            
            services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            services.AddScoped<IExecutionContext, ExecutionContext>();
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
                opt.UseInMemoryDatabase("memoryDb");
            });
            return services;
        }


    }
}
