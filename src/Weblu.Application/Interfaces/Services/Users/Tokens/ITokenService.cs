using Weblu.Application.DTOs.Users.Tokens.RefreshTokenDTOs;
using Weblu.Application.DTOs.Users.Tokens.TokenDTOs;

namespace Weblu.Application.Services.Interfaces.Users.Tokens
{
    public interface ITokenService
    {
        public Task<TokenDTO> RefreshToken(TokenRequestDTO addTokenRequestDTO);
        public Task RevokeToken(RevokeRequestDTO revokeRequestDTO, string userId);
    }
}