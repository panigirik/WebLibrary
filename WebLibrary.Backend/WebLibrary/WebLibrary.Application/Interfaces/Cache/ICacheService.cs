namespace WebLibrary.Application.Interfaces.Cache;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpireTime = null);
    
    Task<T?> GetAsync<T>(string key);
    
    Task RemoveAsync(string key);
}
