using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;

namespace Weblu.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Task<TokenDto> RefreshToken(TokenRequestDto addTokenRequestDto);
        public Task RevokeToken(RevokeRequestDto revokeRequestDto , string userId);

    }
}