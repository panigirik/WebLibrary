using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebLibrary.Indentity.Extensions;

public static class AddSwaggerAuthenticationExtension
{
    /// <summary>
    /// Метод для добавления конфигурации Swagger для аутентификации с использованием JWT.
    /// </summary>
    /// <param name="services">Коллекция сервисов для DI.</param>
    public static void AddSwaggerAuthentication(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}