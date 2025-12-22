using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Identity.Services;
using Weblu.Infrastructure.Token;
using Xunit;

namespace Weblu.Infrastructure.UnitTests.Identity.Services
{
    public class AuthServiceTests
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public AuthServiceTests()
        {
            _userManager = A.Fake<UserManager<AppUser>>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _jwtTokenService = A.Fake<IJwtTokenService>();
            _signInManager = A.Fake<SignInManager<AppUser>>();
            _refreshTokenRepository = A.Fake<IRefreshTokenRepository>();
            _userRepository = A.Fake<IUserRepository>();
        }
        [Fact]
        public async Task AuthService_LoginAsync_ReturnAuthResponseDto()
        {
            // Arrange
            LoginDto loginDto = new LoginDto
            {
                Username = "TestUsername",
                Password = "TestPassword",
            };
            var user = new AppUser()
            {
                FirstName = "Mohammad",
                LastName = "Azimifar",
                PhoneNumber = "989031883414",
                UserName = "TestUsername",
            };
            List<string> roles = new List<string>() { "User" };
            A.CallTo(() => _userManager.FindByNameAsync(loginDto.Username)).Returns(user);
            A.CallTo(() => _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true)).Returns(SignInResult.Success);
            A.CallTo(() => _userManager.GetRolesAsync(user)).Returns(roles);

            A.CallTo(() => _jwtTokenService.GenerateAccessToken(user, roles)).Returns("valid-access-token");
            A.CallTo(() => _jwtTokenService.GenerateRefreshToken()).Returns("valid-refresh-token");

            var authService = new AuthService(_userRepository, _jwtTokenService, _userManager, _unitOfWork, _signInManager, _refreshTokenRepository);

            // Act 
            var act = await authService.LoginAsync(loginDto);

            // Assert
            act.Should().NotBeNull();
            act.Should().BeOfType<AuthResponseDto>();
            act.AccessToken.Should().Be("valid-access-token");
            act.RefreshToken.Should().Be("valid-refresh-token");
            A.CallTo(() => _unitOfWork.CommitAsync())
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _refreshTokenRepository.Add(A<RefreshToken>.Ignored))
                .MustHaveHappenedOnceExactly();

        }
        [Fact]
        public async Task AuthService_RegisterAsync_ReturnAuthResponseDto()
        {
            // Arrange
            RegisterDto registerDto = new RegisterDto
            {
                Username = "TestUsername",
                Password = "TestPassword",
                FirstName = "Mohammad",
                LastName = "Azimifar",
                PhoneNumber = "989031883414"
            };
            var userType = UserType.User;
            List<string> roles = new List<string> { "User" };


            A.CallTo(() => _userManager.FindByNameAsync(registerDto.Username)).Returns(Task.FromResult<AppUser?>(null));
            A.CallTo(() => _userRepository.ExistsWithPhoneAsync(registerDto.PhoneNumber)).Returns(false);

            A.CallTo(() => _userManager.CreateAsync(A<AppUser>._, A<string>._))
                .Returns(IdentityResult.Success);

            A.CallTo(() => _userManager.AddToRoleAsync(A<AppUser>._, userType.ToString())).Returns(IdentityResult.Success);
            A.CallTo(() => _userManager.GetRolesAsync(A<AppUser>._)).Returns(roles);

            A.CallTo(() => _jwtTokenService.GenerateAccessToken(A<AppUser>._, roles)).Returns("valid-access-token");
            A.CallTo(() => _jwtTokenService.GenerateRefreshToken()).Returns("valid-refresh-token");

            var authService = new AuthService(_userRepository, _jwtTokenService, _userManager, _unitOfWork, _signInManager, _refreshTokenRepository);

            // Act
            var act = await authService.RegisterAsync(registerDto, userType);

            // Assert
            act.Should().NotBeNull();
            act.Should().BeOfType<AuthResponseDto>();
            act.AccessToken.Should().Be("valid-access-token");
            act.RefreshToken.Should().Be("valid-refresh-token");

            A.CallTo(() => _refreshTokenRepository.Add(A<RefreshToken>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync())
                .MustHaveHappenedOnceExactly();
        }
    }
}