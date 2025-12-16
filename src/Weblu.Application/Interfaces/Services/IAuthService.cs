using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.AuthDtos;
using Weblu.Domain.Enums.Users;

namespace Weblu.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto , UserType userType );
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}