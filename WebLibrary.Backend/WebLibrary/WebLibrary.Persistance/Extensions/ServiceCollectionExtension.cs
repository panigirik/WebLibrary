using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.Domain.Interfaces;
using WebLibrary.Persistance.Repositories;

namespace WebLibrary.Persistance.Extensions;

public static class ServiceCollectionExtension
{
    
    public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        
            
    }
    
    public static void AddInfrastructureRepositoriesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}