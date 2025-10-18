using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Exceptions;

namespace Weblu.Api.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequestDto)
        {
            TokenDto tokenDto = await _tokenService.RefreshToken(tokenRequestDto);
            return Ok(new
            {
                message = "Tokens refreshed successfully",
                tokens = tokenDto
            });
        }
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeRequestDto revokeRequestDto)
        {
            string? userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException("");
            }
            await _tokenService.RevokeToken(revokeRequestDto, userId);
            return Ok(new
            {
                message = "Tokens revoked successfully",
            });
        }
    }
}