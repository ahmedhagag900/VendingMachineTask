using FlapKap.API.Configuration;
using FlapKap.API.Constants;
using FlapKap.Application.IoC;
using FlapKap.Core;
using FlapKap.Core.Enums;
using FlapKap.Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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

            var settings=builder.Configuration.Get<VendingMachineSettings>();
            builder.Services.AddSingleton(typeof(VendingMachineSettings), settings);

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JWTOptions.Issuer,
                    ValidAudience = settings.JWTOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JWTOptions.SecretKey)),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    //ValidateLifetime=true
                };
            });

            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy(Policy.Seller, builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.RequireClaim(ClaimTypes.Role, ((int)UserRole.Seller).ToString());
                });
                option.AddPolicy(Policy.Buyer, builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.RequireClaim(ClaimTypes.Role, ((int)UserRole.Buyer).ToString());
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        

    }
}