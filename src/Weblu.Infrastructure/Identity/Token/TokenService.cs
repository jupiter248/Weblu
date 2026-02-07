using Microsoft.AspNetCore.Identity;
using Weblu.Application.Dtos.Users.Tokens.RefreshTokenDtos;
using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.Tokens;
using Weblu.Application.Services.Interfaces.Users.Tokens;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Entities;


namespace Weblu.Infrastructure.Identity.Token
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleRepository _roleRepository;

        public TokenService(IRoleRepository roleRepository, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IRefreshTokenRepository refreshTokenRepository, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _roleRepository = roleRepository;
        }
        public async Task<TokenDto> RefreshToken(TokenRequestDto addTokenRequestDto)
        {
            RefreshToken refreshToken = await _refreshTokenRepository.GetByTokenAsync(addTokenRequestDto.RefreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
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
            IList<string> permissions = await _roleRepository.GetRolePermissionsAsync(roles);
            refreshToken.IsUsed = true;
            _refreshTokenRepository.Update(refreshToken);


            var newAccessToken = _jwtTokenService.GenerateAccessToken(appUser, roles, permissions);
            var newRefreshTokenValue = _jwtTokenService.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                Token = newRefreshTokenValue,
                UserId = appUser.Id,
                ExpiresAt = refreshToken.ExpiresAt
            };

            _refreshTokenRepository.Add(newRefreshToken);
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
            RefreshToken refreshToken = await _refreshTokenRepository.GetByTokenAsync(revokeRequestDto.RefreshToken) ?? throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);

            if (refreshToken.UserId != appUser.Id)
            {
                throw new NotFoundException(TokenErrorCodes.RefreshTokenNotFound);
            }

            refreshToken.IsRevoked = true;
            _refreshTokenRepository.Update(refreshToken);
            await _unitOfWork.CommitAsync();
        }
    }
}