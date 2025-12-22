using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.UserDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Enums.Users;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ChangeUserPasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var authorizedUser = _httpContextAccessor.HttpContext?.User;
            string? authorizedUserId = authorizedUser?.GetUserId();
            if (authorizedUser == null)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (authorizedUserId != userId || !authorizedUser.IsInRole(UserType.User.ToString()))
            {
                throw new BadRequestException(UserErrorCodes.UserDeleteForbidden);
            }
            AppUser currentUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);

            bool checkOldPass = await _userManager.CheckPasswordAsync(currentUser, changePasswordDto.OldPassword);
            if (!checkOldPass)
            {   
                throw new UnauthorizedException(UserErrorCodes.OldPasswordIsIncorrect);
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(currentUser, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                throw new UnauthorizedException(UserErrorCodes.UserChangePasswordFailed);
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            var authorizedUser = _httpContextAccessor.HttpContext?.User;
            string? authorizedUserId = authorizedUser?.GetUserId();

            if (authorizedUser == null)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (authorizedUserId != userId || !authorizedUser.IsInRole(UserType.User.ToString()))
            {
                throw new BadRequestException(UserErrorCodes.UserDeleteForbidden);
            }

            AppUser currentUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            await _userManager.DeleteAsync(currentUser);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<AppUser> users = await _userManager.Users.Include(p => p.Profiles).ToListAsync();
            List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            foreach (UserDto userDto in userDtos)
            {
                foreach (AppUser user in users)
                {
                    userDto.Roles.AddRange(await _userManager.GetRolesAsync(user));
                    break;
                }
            }
            return userDtos;
        }

        public async Task<UserDto> GetCurrentUserAsync(string userId)
        {
            AppUser user = await _userManager.Users.Include(p => p.Profiles).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Roles.AddRange(roles);
            return userDto;
        }

        public async Task<string?> GetUsernameAsync(string senderId)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(senderId);
            if (appUser == null)
            {
                return null;
            }
            return appUser.UserName;
        }

        public async Task<bool> IsAdminAsync(string senderId)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(senderId);
            if (appUser == null)
            {
                return false;
            }
            bool isAdmin = await _userManager.IsInRoleAsync(appUser, UserType.Admin.ToString());
            return isAdmin;
        }

        public async Task<UserDto> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
        {
            var authorizedUser = _httpContextAccessor.HttpContext?.User;
            string? authorizedUserId = authorizedUser?.GetUserId();

            if (authorizedUser == null)
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            if (authorizedUserId != userId || !authorizedUser.IsInRole(UserType.User.ToString()))
            {
                throw new BadRequestException(UserErrorCodes.UserUpdateForbidden);
            }

            AppUser currentUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            // IList<string> roles = await _userManager.GetRolesAsync(user);
            currentUser = _mapper.Map(updateUserDto, currentUser);
            await _userManager.UpdateAsync(currentUser);
            UserDto userDto = _mapper.Map<UserDto>(currentUser);
            return userDto;
        }
    }
}