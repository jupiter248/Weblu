using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.Tokens
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDto>> GetAllAsync(RefreshTokenParameters refreshTokenParameters);
        Task<RefreshTokenDto> UpdateAsync(int refreshTokenId, UpdateRefreshTokenDto updateRefreshTokenDto);
        Task<RefreshTokenDto> GetByTokenAsync(string refreshToken);
    }
}