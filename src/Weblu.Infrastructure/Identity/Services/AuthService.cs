using Microsoft.AspNetCore.Identity;
using Weblu.Application.DTOs.Auth.AuthDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Repositories.Users.Roles;
using Weblu.Application.Interfaces.Repositories.Users.Tokens;
using Weblu.Application.Interfaces.Services.Auth;
using Weblu.Domain.Entities.Users.Tokens;
using Weblu.Domain.Enums.Users;
using Weblu.Domain.Errors.Auth;
using Weblu.Domain.Errors.Users;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Identity.Token;

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
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            AppUser appUser = await _userManager.FindByNameAsync(loginDTO.Username.ToLowerInvariant()) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
            SignInResult checkPass = await _signInManager.CheckPasswordSignInAsync(appUser, loginDTO.Password, true);
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

            AuthResponseDTO authResponseDTO = new AuthResponseDTO()
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                FullName = $"{appUser.FirstName} {appUser.LastName}",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                PhoneNumber = appUser.PhoneNumber,
                Username = appUser.UserName
            };

            return authResponseDTO;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO, UserType userType)
        {

            AppUser? foundedByUsername = await _userManager.FindByNameAsync(registerDTO.Username.ToLowerInvariant());
            if (foundedByUsername != null)
            {
                throw new ConflictException(AuthErrorCodes.UsernameAlreadyUsed);
            }
            bool foundedByPhone = await _userRepository.ExistsWithPhoneAsync(registerDTO.PhoneNumber);
            if (foundedByPhone == true)
            {
                throw new ConflictException(AuthErrorCodes.PhoneNumberAlreadyUsed);
            }

            AppUser newUser = new AppUser()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Username.ToLowerInvariant()
            };

            IdentityResult userCreated = await _userManager.CreateAsync(newUser, registerDTO.Password);
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

            AuthResponseDTO authResponseDTO = new AuthResponseDTO()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                FullName = $"{newUser.FirstName} {newUser.LastName}",
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                PhoneNumber = newUser.PhoneNumber,
                Username = newUser.UserName
            };
            return authResponseDTO;
        }
    }
}