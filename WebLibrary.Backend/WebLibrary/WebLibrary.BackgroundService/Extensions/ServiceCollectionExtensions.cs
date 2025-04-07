using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WebLibrary.Application.Redis;
using WebLibrary.BackgroundService.ClamAV;
using WebLibrary.BackgroundService.Services;

namespace WebLibrary.BackgroundService.Extensions;

    /// <summary>
    /// Методы расширения для добавления служб инфраструктуры.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление служб для фоновых задач.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        public static void AddInfrastructureBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<OverdueBookNotificationService>();
            services.AddScoped<RedisCacheService>();
            services.AddDistributedMemoryCache();
            services.AddScoped<ScanFileForMalwareHelper>();

        }
    }
