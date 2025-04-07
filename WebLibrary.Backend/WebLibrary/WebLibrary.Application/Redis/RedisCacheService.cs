using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WebLibrary.Application.Interfaces.Cache;

namespace WebLibrary.Application.Redis;

    /// <summary>
    /// Сервис для работы с кэшем Redis.
    /// </summary>
    public class RedisCacheService: ICacheService
    {
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Конструктор для инициализации с кэшем Redis.
        /// </summary>
        /// <param name="cache">Объект кэша Redis.</param>
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Сохранение данных в Redis.
        /// </summary>
        /// <typeparam name="T">Тип данных для сохранения.</typeparam>
        /// <param name="key">Ключ для данных.</param>
        /// <param name="value">Данные для сохранения.</param>
        /// <param name="options">Опции для записи в кэш.</param>
        public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromHours(1)
            };

            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options);
        }
        
        /// <summary>
        /// Получение данных из Redis.
        /// </summary>
        /// <typeparam name="T">Тип данных, которые нужно получить.</typeparam>
        /// <param name="key">Ключ для данных.</param>
        /// <returns>Данные из кэша или null, если данные не найдены.</returns>
        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            return jsonData != null ? JsonSerializer.Deserialize<T>(jsonData) : default;
        }

        /// <summary>
        /// Удаление данных из Redis.
        /// </summary>
        /// <param name="key">Ключ для удаления.</param>
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
