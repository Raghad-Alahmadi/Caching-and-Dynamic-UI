using Microsoft.Extensions.Caching.Memory;

namespace Caching_and_Dynamic_UI.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null)
        {
            if (_memoryCache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }

            var data = await factory();

            var cacheOptions = new MemoryCacheEntryOptions();
            if (expiry.HasValue)
            {
                cacheOptions.SetAbsoluteExpiration(expiry.Value);
            }
            else
            {
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(5)); // Default expiration
            }

            _memoryCache.Set(key, data, cacheOptions);
            return data;
        }

        public Task<bool> RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.FromResult(true);
        }

        public Task<bool> KeyExistsAsync(string key)
        {
            return Task.FromResult(_memoryCache.TryGetValue(key, out _));
        }
    }
}