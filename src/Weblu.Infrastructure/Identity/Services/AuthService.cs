using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Services.Interfaces;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Enums.Users;
using Weblu.Domain.Errors.Auth;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Authorization;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IRoleRepository roleRepository, IUserRepository userRepository, IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByNameAsync(loginDto.Username.ToLowerInvariant()) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            SignInResult checkPass = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, true);
            if (!checkPass.Succeeded)
            {
                throw new UnauthorizedException(AuthErrorCodes.IncorrectPassword);
            }

            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            IList<string> permissions = await _roleRepository.GetRolePermissionsAsync(roles);

            string newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            string newAccessToken = _jwtTokenService.GenerateAccessToken(appUser, roles, permissions);

            RefreshToken refreshToken = new RefreshToken()
            {
                Token = newRefreshToken,
                ExpiresAt = DateTimeOffset.Now.AddDays(30),
                UserId = appUser.Id
            };
            _refreshTokenRepository.Add(refreshToken);
            await _unitOfWork.CommitAsync();

            AuthResponseDto authResponseDto = new AuthResponseDto()
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                FullName = $"{appUser.FirstName} {appUser.LastName}",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                PhoneNumber = appUser.PhoneNumber,
                Username = appUser.UserName
            };

            return authResponseDto;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto, UserType userType)
        {

            AppUser? foundedByUsername = await _userManager.FindByNameAsync(registerDto.Username.ToLowerInvariant());
            if (foundedByUsername != null)
            {
                throw new ConflictException(AuthErrorCodes.UsernameAlreadyUsed);
            }
            bool foundedByPhone = await _userRepository.ExistsWithPhoneAsync(registerDto.PhoneNumber);
            if (foundedByPhone == true)
            {
                throw new ConflictException(AuthErrorCodes.PhoneNumberAlreadyUsed);
            }

            AppUser newUser = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Username.ToLowerInvariant()
            };

            IdentityResult userCreated = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!userCreated.Succeeded)
            {
                throw new UnauthorizedException(AuthErrorCodes.UserCreationFailed);
            }

            IdentityResult roleAdded = new IdentityResult();

            if (userType == UserType.Admin)
            {
                roleAdded = await _userManager.AddToRoleAsync(newUser, UserType.Admin.ToString());
            }
            else if (userType == UserType.User)
            {
                roleAdded = await _userManager.AddToRoleAsync(newUser, UserType.User.ToString());
            }
            else if (userType == UserType.Editor)
            {
                roleAdded = await _userManager.AddToRoleAsync(newUser, UserType.Editor.ToString());
            }
            if (!roleAdded.Succeeded)
            {
                throw new UnauthorizedException(AuthErrorCodes.RoleAddingFailed);
            }

            IList<string> roles = await _userManager.GetRolesAsync(newUser);
            IList<string> permissions = await _roleRepository.GetRolePermissionsAsync(roles);

            string newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            string newAccessToken = _jwtTokenService.GenerateAccessToken(newUser, roles, permissions);

            RefreshToken refreshToken = new RefreshToken()
            {
                Token = newRefreshToken,
                ExpiresAt = DateTimeOffset.Now.AddDays(30),
                UserId = newUser.Id
            };
            _refreshTokenRepository.Add(refreshToken);
            await _unitOfWork.CommitAsync();

            AuthResponseDto authResponseDto = new AuthResponseDto()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                PhoneNumber = newUser.PhoneNumber,
                Username = newUser.UserName
            };
            return authResponseDto;
        }
    }
}