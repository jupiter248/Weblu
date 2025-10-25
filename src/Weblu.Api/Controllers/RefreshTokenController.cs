using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> GetAllRefreshTokens([FromQuery] RefreshTokenParameters refreshTokenParameters)
        {
            List<RefreshTokenDto> refreshTokenDtos = await _refreshTokenService.GetAllRefreshTokensAsync(refreshTokenParameters);
            return Ok(refreshTokenDtos);
        }
    }
}