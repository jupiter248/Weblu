using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Weblu.Application.Dtos.RefreshTokenDtos;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Users;
using Weblu.Infrastructure.Identity.Authorization;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.UnitTests.Token
{
    public class TokenServiceTests
    {
        // private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly RefreshToken _refreshToken;
        private readonly AppUser _user;
        public readonly List<string> _permissions;
        private readonly IRoleRepository _roleRepository;

        public TokenServiceTests()
        {
            _jwtTokenService = A.Fake<IJwtTokenService>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _refreshTokenRepository = A.Fake<IRefreshTokenRepository>();
            _userManager = A.Fake<UserManager<AppUser>>();
            _roleRepository = A.Fake<IRoleRepository>();
            _user = new AppUser()
            {
                FirstName = "Mohammad",
                LastName = "Azimifar",
                PhoneNumber = "989031883414",
                UserName = "TestUsername",
            };
            _refreshToken = new RefreshToken()
            {
                ExpiresAt = DateTimeOffset.Now.AddMinutes(30),
                Token = "valid-refresh-token",
                IsRevoked = false,
                IsUsed = false,
                UserId = _user.Id,
            };
            _permissions = new List<string> { Permissions.ManageComments };

        }
        [Fact]
        public async Task TokenService_RefreshToken_ReturnValidRefreshToken()
        {
            // Arrange
            List<string> roles = new List<string>() { "User" };

            var tokenRequestDto = new TokenRequestDto()
            {
                RefreshToken = "valid-refresh-token"
            };

            A.CallTo(() => _refreshTokenRepository.GetByTokenAsync(tokenRequestDto.RefreshToken)).Returns(_refreshToken);
            A.CallTo(() => _userManager.FindByIdAsync(_user.Id)).Returns(_user);
            A.CallTo(() => _userManager.GetRolesAsync(_user)).Returns(roles);
            A.CallTo(() => _jwtTokenService.GenerateRefreshToken()).Returns("Valid_Refresh_Token");
            A.CallTo(() => _jwtTokenService.GenerateAccessToken(_user, roles, _permissions)).Returns("Valid_Access_Token");
            A.CallTo(() => _roleRepository.GetRolePermissionsAsync(roles)).Returns(_permissions);


            var tokenService = new TokenService(_roleRepository, _unitOfWork, _userManager, _refreshTokenRepository, _jwtTokenService);

            // Act
            var act = await tokenService.RefreshToken(tokenRequestDto);

            // Assert
            act.Should().NotBeNull();
            act.Should().BeOfType(typeof(TokenDto));
            act.RefreshToken.Should().Be("Valid_Refresh_Token");
            act.AccessToken.Should().Be("Valid_Access_Token");

            A.CallTo(() => _unitOfWork.CommitAsync())
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _refreshTokenRepository.Update(A<RefreshToken>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _refreshTokenRepository.Add(A<RefreshToken>.Ignored))
                .MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task TokenService_RevokeToken_RevokeAndCommit()
        {
            // Arrange
            var revokeRequestDto = new RevokeRequestDto()
            {
                RefreshToken = "valid-refresh-token"
            };
            A.CallTo(() => _userManager.FindByIdAsync(_user.Id)).Returns(_user);
            A.CallTo(() => _refreshTokenRepository.GetByTokenAsync(revokeRequestDto.RefreshToken)).Returns(_refreshToken);

            var tokenService = new TokenService(_roleRepository, _unitOfWork, _userManager, _refreshTokenRepository, _jwtTokenService);
            // Act
            await tokenService.RevokeToken(revokeRequestDto, _user.Id);


            // Assert
            _refreshToken.IsRevoked.Should().BeTrue();

            A.CallTo(() => _refreshTokenRepository.Update(A<RefreshToken>.Ignored))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync())
                .MustHaveHappenedOnceExactly();
        }
    }
}