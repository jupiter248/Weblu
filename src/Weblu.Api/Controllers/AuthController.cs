using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Auth;
using Weblu.Domain.Enums.Users;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
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