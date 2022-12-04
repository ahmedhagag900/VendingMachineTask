using VendingMachine.API.APIRequests.User;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.OpenApi.Models;
using VendingMachine.API.Constants;
using VendingMachine.Core.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using VendingMachine.Infrastructure;
using VendingMachine.Core;
using Microsoft.AspNetCore.Authorization;
using VendingMachine.API.Auth;

namespace VendingMachine.API.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection RegisterBaseServices(this IServiceCollection services,VendingMachineSettings settings)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement

                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });


            services.AddHttpContextAccessor();

            // register request validations
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });
            services.AddValidatorsFromAssembly(typeof(DepositAPIRequestValidator).Assembly);


            services.AddScoped<IAuthorizationHandler, UserRoleAutherizationHandler>();
            //services.AddScoped<IAuthorizationRequirement, UserRoleRequirement>();

            //configure authentication schema to be jwt and define how to validate it.
            services.AddAuthentication(opt =>
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

            //register autherization policies
            services.AddAuthorization(option =>
            {
                //seller policy
                option.AddPolicy(Policy.Seller, builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.Requirements.Add(new UserRoleRequirement((int)UserRole.Seller));
                });
                //buyer policy
                option.AddPolicy(Policy.Buyer, builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.Requirements.Add(new UserRoleRequirement((int)UserRole.Buyer));
                });
                //super admin policy (not used)
                option.AddPolicy(Policy.SA, builder =>
                {
                    builder.RequireAuthenticatedUser();
                    builder.Requirements.Add(new UserRoleRequirement((int)UserRole.SA));
                });
            });


            return services;
        }
    }
}
