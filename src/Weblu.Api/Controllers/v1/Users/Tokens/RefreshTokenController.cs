using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Interfaces.Services.Users.Tokens;
using Weblu.Application.Parameters.Users;

namespace Weblu.Api.Controllers.v1.Tokens
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/refresh-token")]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        public RefreshTokenController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllRefreshTokens([FromQuery] RefreshTokenParameters refreshTokenParameters)
        {
            List<RefreshTokenDto> refreshTokenDtos = await _refreshTokenService.GetAllRefreshTokensAsync(refreshTokenParameters);
            return Ok(refreshTokenDtos);
        }
        [Authorize]
        [HttpPut("{refreshTokenId:int}")]
        public async Task<IActionResult> UpdateRefreshToken(int refreshTokenId, [FromBody] UpdateRefreshTokenDto updateRefreshTokenDto)
        {
            RefreshTokenDto refreshTokenDto = await _refreshTokenService.UpdateRefreshToken(refreshTokenId, updateRefreshTokenDto);
            return Ok(
                ApiResponse<RefreshTokenDto>.Success(
                    "Refresh token updated successfully",
                    refreshTokenDto
                )
            );
        }
    }
}