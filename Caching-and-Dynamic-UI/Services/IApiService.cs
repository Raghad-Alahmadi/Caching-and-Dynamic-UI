using Caching_and_Dynamic_UI.Models.API;

namespace Caching_and_Dynamic_UI.Services
{
    public interface IApiService
    {
        Task<List<User>> GetUsersAsync();
        Task<List<Post>> GetPostsAsync();

    }
}