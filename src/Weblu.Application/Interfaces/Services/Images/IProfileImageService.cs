using Weblu.Application.Dtos.Images.ProfileDtos;
using Weblu.Application.Parameters.Images;
namespace Weblu.Application.Interfaces.Services.Images
{
    public interface IProfileImageService
    {
        Task<List<ProfileDto>> GetAllAsync(ProfileMediaParameters profileMediaParameters);
        Task<ProfileDto> GetByIdAsync(int profileId);
        Task<ProfileDto> AddAsync(AddProfileDto addProfileDto);
        Task DeleteAsync(int profileId);
    }
}