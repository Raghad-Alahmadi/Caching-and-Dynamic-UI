namespace Caching_and_Dynamic_UI.Services
{
    public interface ICacheService
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null);
        Task<bool> RemoveAsync(string key);
        Task<bool> KeyExistsAsync(string key);
    }
}