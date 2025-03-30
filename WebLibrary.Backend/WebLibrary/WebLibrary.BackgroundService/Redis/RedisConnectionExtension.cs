using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace WebLibrary.BackgroundService.Redis;

    public class RedisConnectionExtension
    {
        private readonly string _connectionString;

        public RedisConnectionExtension(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RedisConnection");
        }

        public IConnectionMultiplexer Connect()
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        }
    }
