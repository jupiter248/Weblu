using Weblu.Application.Dtos.About.SocialMediaDtos;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
{
    public interface ISocialMediaService
    {
        Task<List<SocialMediaDto>> GetAllAsync(SocialMediaParameters socialMediaParameters);
        Task<SocialMediaDto> GetByIdAsync(int socialMediaId);
        Task<SocialMediaDto> CreateAsync(CreateSocialMediaDto createSocialMediaDto);
        Task<SocialMediaDto> UpdateAsync(int socialMediaId, UpdateSocialMediaDto updateSocialMediaDto);
        Task<SocialMediaDto> ChangeIconAsync(int socialMediaId, ChangeSocialMediaIconDto changeSocialMediaIconDto);
        Task DeleteAsync(int socialMediaId);
    }
}