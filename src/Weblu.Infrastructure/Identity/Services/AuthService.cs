using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Users;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Identity.Entities;
using Weblu.Infrastructure.Token;

namespace Weblu.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByNameAsync(loginDto.Username) ?? throw new NotFoundException("");
            SignInResult checkPass = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, true);
            if (!checkPass.Succeeded)
            {
                throw new UnauthorizedException("");
            }
            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            string newRefreshToken = JwtTokenService.GenerateRefreshToken();
            string newAccessToken = JwtTokenService.GenerateAccessToken(appUser, roles);

            RefreshToken refreshToken = new RefreshToken()
            {
                Token = newRefreshToken,
                ExpiresAt = DateTimeOffset.Now.AddMonths(1),
                UserId = appUser.Id
            };
            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(refreshToken);
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
            AppUser? foundByUsername = await _userManager.FindByNameAsync(registerDto.Username);
            if (foundByUsername != null)
            {
                throw new ConflictException("");
            }
            AppUser? foundByPhone = await _userManager.Users.FirstOrDefaultAsync(e => e.PhoneNumber == registerDto.PhoneNumber);
            if (foundByPhone != null)
            {
                throw new ConflictException("");
            }

            AppUser newUser = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.PhoneNumber
            };

            IdentityResult userCreated = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!userCreated.Succeeded)
            {
                throw new UnauthorizedException("");
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

            if (!roleAdded.Succeeded)
            {
                throw new UnauthorizedException("");
            }

            IList<string> roles = await _userManager.GetRolesAsync(newUser);

            string newRefreshToken = JwtTokenService.GenerateRefreshToken();
            string newAccessToken = JwtTokenService.GenerateAccessToken(newUser, roles);

            RefreshToken refreshToken = new RefreshToken()
            {
                Token = newRefreshToken,
                ExpiresAt = DateTimeOffset.Now.AddMonths(1),
                UserId = newUser.Id
            };
            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(refreshToken);
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