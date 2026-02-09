using Weblu.Application.Dtos.Users.UserDtos;

namespace Weblu.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllAsync();
        public Task<UserDto> GetCurrentAsync(string userId);
        public Task<UserDto> UpdateAsync(string userId, UpdateUserDto updateUserDto);
        public Task DeleteAsync(string userId);
        public Task ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        public Task<bool> IsAdminAsync(string senderId);
        public Task<string?> GetUsernameAsync(string senderId);
    }
}