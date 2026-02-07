using Weblu.Application.Dtos.About.SocialMediaDtos;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
{
    public interface ISocialMediaService
    {
        Task<List<SocialMediaDto>> GetAllSocialMediasAsync(SocialMediaParameters socialMediaParameters);
        Task<SocialMediaDto> GetSocialMediaByIdAsync(int socialMediaId);
        Task<SocialMediaDto> AddSocialMediaAsync(AddSocialMediaDto addSocialMediaDto);
        Task<SocialMediaDto> UpdateSocialMediaAsync(int socialMediaId, UpdateSocialMediaDto updateSocialMediaDto);
        Task<SocialMediaDto> UpdateSocialMediaIconAsync(int socialMediaId, UpdateSocialMediaIconDto updateSocialMediaIcon);
        Task DeleteSocialMediaAsync(int socialMediaId);
    }
}