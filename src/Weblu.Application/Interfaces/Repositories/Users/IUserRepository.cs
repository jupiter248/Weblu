using Weblu.Application.DTOs.Articles.CommentDTOs;

namespace Weblu.Application.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string userId);
        Task<CommentUserDTO?> GetUserForCommentAsync(string userId);
        Task<bool> IsAdminAsync(string userId);
        Task<bool> ExistsWithPhoneAsync(string phoneNumber);
    }
}