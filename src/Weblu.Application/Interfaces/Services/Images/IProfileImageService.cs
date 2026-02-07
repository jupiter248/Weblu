using Weblu.Application.Dtos.Images.ProfileDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Images;
using Weblu.Application.Services;
namespace Weblu.Application.Interfaces.Services.Images
{
    public interface IProfileImageService
    {
        Task<List<ProfileDto>> GetAllProfilesAsync(ProfileMediaParameters profileMediaParameters);
        Task<ProfileDto> GetProfileByIdAsync(int profileId);
        Task<ProfileDto> AddProfileAsync(AddProfileDto addProfileDto);
        Task DeleteProfileAsync(int profileId);
    }
}