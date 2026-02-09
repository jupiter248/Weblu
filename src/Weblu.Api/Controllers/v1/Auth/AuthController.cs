using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Auth.AuthDTOs;
using Weblu.Application.Interfaces.Services.Auth;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Auth;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Auth
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
        [Authorize(Policy = Permissions.ManageAdmins)]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDTO)
        {
            Validator.ValidateAndThrow(registerDTO, new RegisterValidator());
            AuthResponseDTO authResponseDTO = await _authService.RegisterAsync(registerDTO, UserType.Admin);
            return Ok(ApiResponse<AuthResponseDTO>.Success
            (
                "User created successfully.",
                authResponseDTO
            ));
        }
        [Authorize(Policy = Permissions.ManageAdmins)]
        [HttpPost("register-editor")]
        public async Task<IActionResult> RegisterEditor([FromBody] RegisterDTO registerDTO)
        {
            Validator.ValidateAndThrow(registerDTO, new RegisterValidator());
            AuthResponseDTO authResponseDTO = await _authService.RegisterAsync(registerDTO, UserType.Editor);
            return Ok(ApiResponse<AuthResponseDTO>.Success
            (
                "User created successfully.",
                authResponseDTO
            ));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            Validator.ValidateAndThrow(registerDTO, new RegisterValidator());
            AuthResponseDTO authResponseDTO = await _authService.RegisterAsync(registerDTO, UserType.User);
            return Ok(ApiResponse<AuthResponseDTO>.Success
            (
                "User created successfully.",
                authResponseDTO
            ));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            Validator.ValidateAndThrow(loginDTO, new LoginValidator());
            AuthResponseDTO authResponseDTO = await _authService.LoginAsync(loginDTO);
            return Ok(ApiResponse<AuthResponseDTO>.Success
            (
                "User logged in successfully.",
                authResponseDTO
            ));
        }
    }
}