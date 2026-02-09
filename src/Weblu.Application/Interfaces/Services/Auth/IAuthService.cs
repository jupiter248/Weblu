using Weblu.Application.DTOs.Auth.AuthDTOs;
using Weblu.Domain.Enums.Users;

namespace Weblu.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO, UserType userType);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
    }
}