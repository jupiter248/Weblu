using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.CommentDtos;

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