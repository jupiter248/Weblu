using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Users.Tokens.TokenDTOs;
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
        public async Task<IActionResult> GetAll([FromQuery] RefreshTokenParameters refreshTokenParameters)
        {
            List<RefreshTokenDTO> refreshTokenDTOs = await _refreshTokenService.GetAllAsync(refreshTokenParameters);
            return Ok(refreshTokenDTOs);
        }
        [Authorize]
        [HttpPut("{refreshTokenId:int}")]
        public async Task<IActionResult> Update(int refreshTokenId, [FromBody] UpdateRefreshTokenDTO updateRefreshTokenDTO)
        {
            RefreshTokenDTO refreshTokenDTO = await _refreshTokenService.UpdateAsync(refreshTokenId, updateRefreshTokenDTO);
            return Ok(
                ApiResponse<RefreshTokenDTO>.Success(
                    "Refresh token updated successfully",
                    refreshTokenDTO
                )
            );
        }
    }
}