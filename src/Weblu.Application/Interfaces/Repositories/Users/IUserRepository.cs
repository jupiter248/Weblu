using Weblu.Application.Dtos.Articles.CommentDtos;

namespace Weblu.Application.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string userId);
        Task<CommentUserDto?> GetUserForCommentAsync(string userId);
        Task<bool> IsAdminAsync(string userId);
        Task<bool> ExistsWithPhoneAsync(string phoneNumber);
    }
}