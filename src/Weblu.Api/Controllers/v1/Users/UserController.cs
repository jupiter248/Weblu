using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Users.UserDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Users;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Users;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Users.Tokens
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Policy = Permissions.ManageUsers)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserDto> userDtos = await _userService.GetAllAsync();
            return Ok(userDtos);
        }
        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            UserDto userDto = await _userService.GetCurrentAsync(userId);
            return Ok(userDto);
        }
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserDto updateUserDto)
        {
            Validator.ValidateAndThrow(updateUserDto, new UpdateUserValidator());
            UserDto userDto = await _userService.UpdateAsync(userId, updateUserDto);
            return Ok(ApiResponse<UserDto>.Success
            (
                "User updated successfully.",
                userDto
            ));
        }
        [Authorize]
        [HttpPut("{userId}/change-password")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordDto changePasswordDto)
        {
            Validator.ValidateAndThrow(changePasswordDto, new ChangeUserPasswordValidator());
            await _userService.ChangePasswordAsync(userId, changePasswordDto);
            return Ok(ApiResponse.Success
            (
                "User password changed successfully."
            ));
        }
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            await _userService.DeleteAsync(userId);
            return Ok(ApiResponse.Success
            (
                "User deleted successfully."
            ));
        }
    }
}