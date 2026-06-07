using Weblu.Domain.Entities.Users;

namespace Weblu.Domain.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string userId);
        Task<User?> GetUserAsync(string userId);
        Task<bool> IsAdminAsync(string userId);
        Task<bool> ExistsWithPhoneAsync(string phoneNumber);
    }
}