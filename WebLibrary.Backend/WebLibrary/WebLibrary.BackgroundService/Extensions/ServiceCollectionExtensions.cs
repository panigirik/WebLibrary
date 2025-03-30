using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WebLibrary.BackgroundService.ClamAV;
using WebLibrary.BackgroundService.Redis;
using WebLibrary.BackgroundService.Services;

namespace WebLibrary.BackgroundService.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureBackgroundServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<OverdueBookNotificationService>();
        
        services.AddScoped<RedisCacheService>();
        
        services.AddDistributedMemoryCache();

        services.AddScoped<ScanFileForMalwareHelper>();
        
        services.AddSingleton<RedisConnectionExtension>(); 
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisConnectionExtension = sp.GetRequiredService<RedisConnectionExtension>();
            return redisConnectionExtension.Connect(); 
        });

    }
}