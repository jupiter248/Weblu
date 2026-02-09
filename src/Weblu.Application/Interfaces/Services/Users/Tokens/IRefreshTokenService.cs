using Weblu.Application.DTOs.Users.Tokens.TokenDTOs;
using Weblu.Application.Parameters.Users;

namespace Weblu.Application.Interfaces.Services.Users.Tokens
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDTO>> GetAllAsync(RefreshTokenParameters refreshTokenParameters);
        Task<RefreshTokenDTO> UpdateAsync(int refreshTokenId, UpdateRefreshTokenDTO updateRefreshTokenDTO);
        Task<RefreshTokenDTO> GetByTokenAsync(string refreshToken);
    }
}