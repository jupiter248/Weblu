using Weblu.Domain.Entities.Users;

namespace Weblu.Domain.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string userId);
        Task<string?> GetUsernameAsync(string userId);
        Task<Dictionary<string, string>> GetUsernamesByIdsAsync(string[] userIds);
        Task<User?> GetUserAsync(string userId);
        Task<bool> IsAdminAsync(string userId);
        Task<bool> ExistsWithPhoneAsync(string phoneNumber);
    }
}