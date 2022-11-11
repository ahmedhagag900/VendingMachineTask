namespace FlapKap.API.Configuration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureBaseServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder;
        }
    }
}
