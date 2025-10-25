using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Errors.Tokens;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Entities;


namespace Weblu.Infrastructure.Token
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<TokenDto> RefreshToken(TokenRequestDto addTokenRequestDto)
        {
            RefreshToken refreshToken = await _unitOfWork.RefreshTokens.GetRefreshTokenByTokenAsync(addTokenRequestDto.RefreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            if (refreshToken.IsUsed)
            {
                throw new UnauthorizedException(TokenErrorCodes.RefreshTokenUsed);
            }
            if (refreshToken.IsRevoked)
            {
                throw new UnauthorizedException(TokenErrorCodes.RefreshTokenRevoked);
            }
            if (refreshToken.ExpiresAt < DateTimeOffset.Now)
            {
                throw new UnauthorizedException(TokenErrorCodes.RefreshTokenExpired);
            }

            AppUser appUser = await _userManager.FindByIdAsync(refreshToken.UserId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            IList<string> roles = await _userManager.GetRolesAsync(appUser) ?? throw new NotFoundException(UserErrorCodes.RoleNotFound);

            refreshToken.IsUsed = true;
            _unitOfWork.RefreshTokens.UpdateRefreshToken(refreshToken);

            var newAccessToken = JwtTokenService.GenerateAccessToken(appUser, roles);
            var newRefreshTokenValue = JwtTokenService.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                Token = newRefreshTokenValue,
                UserId = appUser.Id,
                ExpiresAt = refreshToken.ExpiresAt
            };

            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(newRefreshToken);
            await _unitOfWork.CommitAsync();

            TokenDto tokenDto = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue
            };
            return tokenDto;
        }

        public async Task RevokeToken(RevokeRequestDto revokeRequestDto, string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            RefreshToken refreshToken = await _unitOfWork.RefreshTokens.GetRefreshTokenByTokenAsync(revokeRequestDto.RefreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);

            if (refreshToken.UserId != appUser.Id)
            {
                throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            }

            refreshToken.IsRevoked = true;
            _unitOfWork.RefreshTokens.UpdateRefreshToken(refreshToken);
            await _unitOfWork.CommitAsync();
        }
    }
}