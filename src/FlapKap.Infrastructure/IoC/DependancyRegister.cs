using VendingMachine.Core;
using VendingMachine.Core.Constatnt;
using VendingMachine.Core.Repositories;
using VendingMachine.Core.UnitOfWork;
using VendingMachine.Infrastructure.PipeLines;
using VendingMachine.Infrastructure.Repositories;
using VendingMachine.Infrastructure.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace VendingMachine.Infrastructure.IoC
{
    public static class DependancyRegister
    {
        private static IServiceCollection RegisterSqlServerDbContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<VendingMachieneContext>(opt =>
            {
                opt.UseSqlServer(connectionString);

            }, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection RegisterInfraStructureServices(this IServiceCollection services,bool inMemotyDb=true,string connectionString="")
        {

            services.AddMediatR(typeof(DependancyRegister).Assembly);

            // if set to ture then regester the in memory data base pipeLine 
            if (inMemotyDb)
            {
                services.RegisterInMemmoryDBContext();
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MemoryDBPipeLine<,>));
            }
            else //regeister the transaction pipe line 
            {
                services.RegisterSqlServerDbContext(connectionString);
                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipeLine<,>));
            }
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


            services.AddSingleton(typeof(Constants));
            services.AddTransient(typeof(ContextSeed));

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
        private static IServiceCollection RegisterInMemmoryDBContext(this IServiceCollection services)
        {
            services.AddDbContext<VendingMachieneContext>(opt =>
            {
                opt.UseInMemoryDatabase("memoryDb");
            });
            return services;
        }


    }
}
