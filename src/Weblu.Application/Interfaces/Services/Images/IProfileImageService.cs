using Weblu.Application.DTOs.Images.ProfileDTOs;
using Weblu.Application.Parameters.Images;
namespace Weblu.Application.Interfaces.Services.Images
{
    public interface IProfileImageService
    {
        Task<List<ProfileDTO>> GetAllAsync(ProfileMediaParameters profileMediaParameters);
        Task<ProfileDTO> GetByIdAsync(int profileId);
        Task<ProfileDTO> AddAsync(AddProfileDTO addProfileDTO);
        Task DeleteAsync(int profileId);
    }
}