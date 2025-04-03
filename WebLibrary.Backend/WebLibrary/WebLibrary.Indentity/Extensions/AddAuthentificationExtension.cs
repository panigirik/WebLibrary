using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebLibrary.Indentity.Extensions;

    public static class AddAuthentificationExtension
    {
        /// <summary>
        /// Extension-метод для добавления инфраструктурных сервисов аутентификации с использованием JWT.
        /// </summary>
        /// <param name="services">Коллекция сервисов для DI.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        public static void AddInfrastructureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                    options.UseSecurityTokenValidators = true;
                    options.SaveToken = false; 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                        Console.WriteLine("Token invalid: " + context.Exception.ToString());
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                return context.Response.WriteAsync("Invalid token.");
                            }
                            else
                            {
                                Console.WriteLine("Response already started, cannot set status code.");
                            }
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Token successfully validated.");
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }