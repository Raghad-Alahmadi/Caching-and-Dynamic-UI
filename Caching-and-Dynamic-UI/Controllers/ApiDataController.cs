using Microsoft.AspNetCore.Mvc;
using Caching_and_Dynamic_UI.Services;
using Caching_and_Dynamic_UI.Models.API;

namespace Caching_and_Dynamic_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDataController : ControllerBase
    {
        private readonly IApiService _apiService;
        private readonly ICacheService _redisCacheService;
        private readonly ICacheService _memoryCacheService;
        private readonly ILogger<ApiDataController> _logger;

        public ApiDataController(
            IApiService apiService,
            [FromKeyedServices("redis")] ICacheService redisCacheService,
            [FromKeyedServices("memory")] ICacheService memoryCacheService,
            ILogger<ApiDataController> logger)
        {
            _apiService = apiService;
            _redisCacheService = redisCacheService;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        // Get users from Redis cache or API
        [HttpGet("redis/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersFromRedis()
        {
            try
            {
                var users = await _redisCacheService.GetOrSetAsync("users",
                    () => _apiService.GetUsersAsync(),
                    TimeSpan.FromMinutes(10));

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users from Redis");
                return StatusCode(500, "Internal server error");
            }
        }

        // Get users from memory cache or API
        [HttpGet("memory/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersFromMemory()
        {
            try
            {
                var users = await _memoryCacheService.GetOrSetAsync("users",
                    () => _apiService.GetUsersAsync(),
                    TimeSpan.FromMinutes(5));

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users from MemoryCache");
                return StatusCode(500, "Internal server error");
            }
        }

        // Get posts from Redis cache or API
        [HttpGet("redis/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsFromRedis()
        {
            try
            {
                var posts = await _redisCacheService.GetOrSetAsync("posts",
                    () => _apiService.GetPostsAsync(),
                    TimeSpan.FromMinutes(10));

                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving posts from Redis");
                return StatusCode(500, "Internal server error");
            }
        }

        // Get posts from memory cache or API
        [HttpGet("memory/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsFromMemory()
        {
            try
            {
                var posts = await _memoryCacheService.GetOrSetAsync("posts",
                    () => _apiService.GetPostsAsync(),
                    TimeSpan.FromMinutes(5));

                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving posts from MemoryCache");
                return StatusCode(500, "Internal server error");
            }
        }

        // Clear Redis cache
        [HttpDelete("redis/clear")]
        public async Task<IActionResult> ClearRedisCache([FromQuery] string key)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest("Key is required");

            await _redisCacheService.RemoveAsync(key);
            return Ok($"Cache for key '{key}' has been cleared");
        }

        // Clear Memory cache
        [HttpDelete("memory/clear")]
        public async Task<IActionResult> ClearMemoryCache([FromQuery] string key)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest("Key is required");

            await _memoryCacheService.RemoveAsync(key);
            return Ok($"Cache for key '{key}' has been cleared");
        }
    }
}
