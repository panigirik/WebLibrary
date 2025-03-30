using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace WebLibrary.BackgroundService.Redis;

public class RedisCacheService
{
    private readonly IDistributedCache _cache;
    
    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, jsonData, options);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var jsonData = await _cache.GetStringAsync(key);
        return jsonData != null ? JsonSerializer.Deserialize<T>(jsonData) : default;
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}