using Weblu.Application.Dtos.Auth.AuthDtos;
using Weblu.Domain.Enums.Users;

namespace Weblu.Application.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto , UserType userType );
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}