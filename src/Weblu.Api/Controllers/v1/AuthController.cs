using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Application.Services.Interfaces;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Auth;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1
{
    [ApiVersion("1")]
    [EnableRateLimiting("AuthPolicy")]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize(Roles = "Head-Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            Validator.ValidateAndThrow(registerDto, new RegisterValidator());
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto, UserType.Admin);
            return Ok(ApiResponse<AuthResponseDto>.Success
            (
                "User created successfully.",
                authResponseDto
            ));
        }
        [Authorize(Roles = "Head-Admin")]
        [HttpPost("register-editor")]
        public async Task<IActionResult> RegisterEditor([FromBody] RegisterDto registerDto)
        {
            Validator.ValidateAndThrow(registerDto, new RegisterValidator());
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto, UserType.Editor);
            return Ok(ApiResponse<AuthResponseDto>.Success
            (
                "User created successfully.",
                authResponseDto
            ));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            Validator.ValidateAndThrow(registerDto, new RegisterValidator());
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto, UserType.User);
            return Ok(ApiResponse<AuthResponseDto>.Success
            (
                "User created successfully.",
                authResponseDto
            ));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            Validator.ValidateAndThrow(loginDto, new LoginValidator());
            AuthResponseDto authResponseDto = await _authService.LoginAsync(loginDto);
            return Ok(ApiResponse<AuthResponseDto>.Success
            (
                "User logged in successfully.",
                authResponseDto
            ));
        }
    }
}