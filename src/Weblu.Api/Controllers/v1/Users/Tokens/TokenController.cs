using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Users.Tokens.RefreshTokenDtos;
using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Services.Interfaces.Users.Tokens;
using Weblu.Domain.Errors.Users;

namespace Weblu.Api.Controllers.v1.Tokens
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [Authorize]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequestDto)
        {
            TokenDto tokenDto = await _tokenService.RefreshToken(tokenRequestDto);
            return Ok(ApiResponse<TokenDto>.Success
            (
                "Token refreshed successfully.",
                tokenDto
            ));
        }
        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeRequestDto revokeRequestDto)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _tokenService.RevokeToken(revokeRequestDto, userId);
            return Ok(ApiResponse.Success
            (
                "Tokens revoked successfully"
            ));
        }
    }
}