using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace WebLibrary.BackgroundService.Redis;

    public class RedisConnectionExtension
    {
        private readonly string _connectionString;

        public RedisConnectionExtension(IConfiguration configuration)
        {
            // Получаем строку подключения к Redis из конфигурации
            _connectionString = configuration.GetConnectionString("RedisConnection");
        }

        public IConnectionMultiplexer Connect()
        {
            // Подключение к Redis
            return ConnectionMultiplexer.Connect(_connectionString);
        }
    }
