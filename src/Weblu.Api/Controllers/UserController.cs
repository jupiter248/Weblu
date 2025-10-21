using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.UserDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Users;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDto> userDtos = await _userService.GetAllUsersAsync();
            return Ok(userDtos);
        }
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            UserDto userDto = await _userService.GetCurrentUserAsync(userId);
            return Ok(userDto);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
        {
            Validator.ValidateAndThrow(updateUserDto, new UpdateUserValidator());
            UserDto userDto = await _userService.UpdateUserAsync(userId, updateUserDto);
            return Ok(ApiResponse<UserDto>.Success
            (
                "User updated successfully.",
                userDto
            ));
        }
        [HttpPut("{userId}/change-password")]
        public async Task<IActionResult> ChangeUserPassword(string userId, [FromBody] ChangePasswordDto changePasswordDto)
        {
            Validator.ValidateAndThrow(changePasswordDto, new ChangeUserPasswordValidator());
            await _userService.ChangeUserPasswordAsync(userId, changePasswordDto);
            return Ok(ApiResponse.Success
            (
                "User password changed successfully."
            ));
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok(ApiResponse.Success
            (
                "User deleted successfully."
            ));
        }
    }
}