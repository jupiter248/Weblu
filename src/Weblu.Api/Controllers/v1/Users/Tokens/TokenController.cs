using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Users.Tokens.RefreshTokenDTOs;
using Weblu.Application.DTOs.Users.Tokens.TokenDTOs;
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
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequestDTO)
        {
            TokenDTO tokenDTO = await _tokenService.RefreshToken(tokenRequestDTO);
            return Ok(ApiResponse<TokenDTO>.Success
            (
                "Token refreshed successfully.",
                tokenDTO
            ));
        }
        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeRequestDTO revokeRequestDTO)
        {
            string? userId = User.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException(UserErrorCodes.UserNotFound);
            }
            await _tokenService.RevokeToken(revokeRequestDTO, userId);
            return Ok(ApiResponse.Success
            (
                "Tokens revoked successfully"
            ));
        }
    }
}