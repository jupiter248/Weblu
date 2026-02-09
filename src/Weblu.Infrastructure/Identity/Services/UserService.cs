using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.DTOs.Users.UserDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Users;
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

        public async Task ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDTO)
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

            bool checkOldPass = await _userManager.CheckPasswordAsync(currentUser, changePasswordDTO.OldPassword);
            if (!checkOldPass)
            {
                throw new UnauthorizedException(UserErrorCodes.OldPasswordIsIncorrect);
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(currentUser, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded)
            {
                throw new UnauthorizedException(UserErrorCodes.UserChangePasswordFailed);
            }
        }

        public async Task DeleteAsync(string userId)
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
            currentUser.Delete();
        }
        public async Task<List<UserDTO>> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.Include(p => p.Profiles).ToListAsync();
            List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users) ?? default!;
            foreach (UserDTO userDTO in userDTOs)
            {
                foreach (AppUser user in users)
                {
                    userDTO.Roles.AddRange(await _userManager.GetRolesAsync(user));
                    break;
                }
            }
            return userDTOs;
        }

        public async Task<UserDTO> GetCurrentAsync(string userId)
        {
            AppUser user = await _userManager.Users.Include(p => p.Profiles).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            UserDTO userDTO = _mapper.Map<UserDTO>(user) ?? default!;
            userDTO.Roles.AddRange(roles);
            return userDTO;
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

        public async Task<UserDTO> UpdateAsync(string userId, UpdateUserDTO updateUserDTO)
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
            currentUser = _mapper.Map(updateUserDTO, currentUser) ?? default!;
            await _userManager.UpdateAsync(currentUser);
            UserDTO userDTO = _mapper.Map<UserDTO>(currentUser) ?? default!;
            return userDTO;
        }
    }
}