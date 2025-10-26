using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDto>> GetAllRefreshTokensAsync(RefreshTokenParameters refreshTokenParameters);
        Task<RefreshTokenDto> UpdateRefreshToken(int refreshTokenId, UpdateRefreshTokenDto updateRefreshTokenDto);
        Task<RefreshTokenDto> GetRefreshTokenByToken(string refreshToken);
    }
}