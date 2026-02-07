using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.Tokens
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDto>> GetAllRefreshTokensAsync(RefreshTokenParameters refreshTokenParameters);
        Task<RefreshTokenDto> UpdateRefreshToken(int refreshTokenId, UpdateRefreshTokenDto updateRefreshTokenDto);
        Task<RefreshTokenDto> GetRefreshTokenByToken(string refreshToken);
    }
}