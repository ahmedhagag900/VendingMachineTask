using FlapKap.API.Configuration;
using FlapKap.Application.IoC;
using FlapKap.Core;
using FlapKap.Infrastructure.IoC;

namespace FlapKap.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder = builder.ConfigureBaseServices();

            builder.Services
                .RegisterInMemmoryDBContext()
                .RegisterApplicationServices()
                .RegisterInfraStructureServices();

            builder.Services.AddHttpContextAccessor();

            var settings=builder.Configuration.Get<VendingMachecineSettings>();
            builder.Services.AddSingleton(typeof(VendingMachecineSettings), settings);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        

    }
}