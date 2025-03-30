using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.Domain.Interfaces;
using WebLibrary.Persistance.Repositories;

namespace WebLibrary.Persistance.Extensions;

/// <summary>
/// Расширения для регистрации зависимостей в контейнере сервисов.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Регистрирует контекст базы данных в DI контейнере.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
    public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }
    
    /// <summary>
    /// Регистрирует все репозитории инфраструктуры.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddInfrastructureRepositoriesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}