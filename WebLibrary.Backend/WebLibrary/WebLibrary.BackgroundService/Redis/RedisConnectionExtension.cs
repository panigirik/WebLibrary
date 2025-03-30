using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace WebLibrary.BackgroundService.Redis;

    /// <summary>
    /// Класс для подключения к Redis.
    /// </summary>
    public class RedisConnectionExtension
    {
        private readonly string _connectionString;

        /// <summary>
        /// Конструктор для инициализации с строкой подключения.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public RedisConnectionExtension(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RedisConnection");
        }

        /// <summary>
        /// Подключение к Redis.
        /// </summary>
        /// <returns>Подключение к Redis.</returns>
        public IConnectionMultiplexer Connect()
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        }
    }
