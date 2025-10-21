using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.UserDtos;

namespace Weblu.Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<UserDto> GetCurrentUserAsync(string userId);
        public Task<UserDto> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        public Task DeleteUserAsync(string userId);

    }
}