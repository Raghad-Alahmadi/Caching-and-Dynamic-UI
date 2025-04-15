using StackExchange.Redis;
using System.Text.Json;

namespace Caching_and_Dynamic_UI.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("RedisConnection");
            var connection = ConnectionMultiplexer.Connect(connectionString);
            _database = connection.GetDatabase();
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null)
        {
            var value = await _database.StringGetAsync(key);
            if (!value.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            var data = await factory();
            var serializedData = JsonSerializer.Serialize(data);
            await _database.StringSetAsync(key, serializedData, expiry);
            return data;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
    }
}