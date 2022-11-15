using FlapKap.API.Configuration;
using FlapKap.API.Middleware;
using FlapKap.Application.IoC;
using FlapKap.Core;
using FlapKap.Infrastructure;
using FlapKap.Infrastructure.IoC;

namespace FlapKap.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            var settings = builder.Configuration.Get<VendingMachineSettings>();

            builder.Services
                .RegisterBaseServices(settings)
                .RegisterApplicationServices()
                .RegisterInfraStructureServices(inMemotyDb: false, settings.ConnectionString);


            builder.Services.Configure<VendingMachineSettings>(builder.Configuration);

            


            

            var app = builder.Build();

            //seed data
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ContextSeed>();
                service.SeedDataAsync(inMemory: false).Wait();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
               // app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        

    }
}