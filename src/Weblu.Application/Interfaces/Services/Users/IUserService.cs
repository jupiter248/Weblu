using Weblu.Application.DTOs.Users.UserDTOs;

namespace Weblu.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        public Task<List<UserDTO>> GetAllAsync();
        public Task<UserDTO> GetCurrentAsync(string userId);
        public Task<UserDTO> UpdateAsync(string userId, UpdateUserDTO updateUserDTO);
        public Task DeleteAsync(string userId);
        public Task ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDTO);
        public Task<bool> IsAdminAsync(string senderId);
        public Task<string?> GetUsernameAsync(string senderId);
    }
}