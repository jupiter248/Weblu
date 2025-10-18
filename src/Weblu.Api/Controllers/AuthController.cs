using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.AuthDtos;
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
            AuthResponseDto authResponseDto = await _authService.RegisterAsync(registerDto, UserType.User);
            return Ok(new
            {
                message = "User created successfully.",
                user = authResponseDto
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto registerDto)
        {
            AuthResponseDto authResponseDto = await _authService.LoginAsync(registerDto);
            return Ok(new
            {
                message = "User logged in successfully.",
                user = authResponseDto
            });
        }
    }
}