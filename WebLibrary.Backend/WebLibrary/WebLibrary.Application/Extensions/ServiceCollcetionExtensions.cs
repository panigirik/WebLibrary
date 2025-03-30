using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Mappings;
using WebLibrary.Application.Services;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Extensions;

/// <summary>
/// Класс расширений для добавления сервисов в контейнер зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистация основных сервисов приложения в коллекции служб.
    /// </summary>
    /// <param name="services">Коллекция служб <see cref="IServiceCollection"/> для конфигурации зависимостей.</param>
    public static void AddCoreApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddAutoMapper(typeof(UserMappingProfile));
        services.AddAutoMapper(typeof(AuthorMappingProfile));
        services.AddAutoMapper(typeof(BookMappingProfile));
        services.AddAutoMapper(typeof(NotificationMappingProfile));
        services.AddAutoMapper(typeof(RefreshTokenMappingProfile));

        services.AddSingleton<JwtTokenService>();
    }
}