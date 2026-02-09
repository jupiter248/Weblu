using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Users.UserDTOs;
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
            List<UserDTO> userDTOs = await _userService.GetAllAsync();
            return Ok(userDTOs);
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
            UserDTO userDTO = await _userService.GetCurrentAsync(userId);
            return Ok(userDTO);
        }
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] UpdateUserDTO updateUserDTO)
        {
            Validator.ValidateAndThrow(updateUserDTO, new UpdateUserValidator());
            UserDTO userDTO = await _userService.UpdateAsync(userId, updateUserDTO);
            return Ok(ApiResponse<UserDTO>.Success
            (
                "User updated successfully.",
                userDTO
            ));
        }
        [Authorize]
        [HttpPut("{userId}/change-password")]
        public async Task<IActionResult> ChangePassword(string userId, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            Validator.ValidateAndThrow(changePasswordDTO, new ChangeUserPasswordValidator());
            await _userService.ChangePasswordAsync(userId, changePasswordDTO);
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