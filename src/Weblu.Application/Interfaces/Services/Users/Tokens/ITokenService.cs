using Weblu.Application.Dtos.Users.Tokens.RefreshTokenDtos;
using Weblu.Application.Dtos.Users.Tokens.TokenDtos;

namespace Weblu.Application.Services.Interfaces.Users.Tokens
{
    public interface ITokenService
    {
        public Task<TokenDto> RefreshToken(TokenRequestDto addTokenRequestDto);
        public Task RevokeToken(RevokeRequestDto revokeRequestDto, string userId);
    }
}