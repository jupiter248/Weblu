using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;

namespace Weblu.Api.Controllers
{
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