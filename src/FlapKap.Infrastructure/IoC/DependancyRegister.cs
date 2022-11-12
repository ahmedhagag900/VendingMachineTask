using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

        private static IServiceCollection RegisterInfraStructureServices(IServiceCollection services)
        {

           // services.AddMediatr

            return services;
        }

    }
}
