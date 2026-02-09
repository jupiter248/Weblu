using Weblu.Application.DTOs.About.SocialMediaDTOs;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
{
    public interface ISocialMediaService
    {
        Task<List<SocialMediaDTO>> GetAllAsync(SocialMediaParameters socialMediaParameters);
        Task<SocialMediaDTO> GetByIdAsync(int socialMediaId);
        Task<SocialMediaDTO> CreateAsync(CreateSocialMediaDTO createSocialMediaDTO);
        Task<SocialMediaDTO> UpdateAsync(int socialMediaId, UpdateSocialMediaDTO updateSocialMediaDTO);
        Task<SocialMediaDTO> ChangeIconAsync(int socialMediaId, ChangeSocialMediaIconDTO changeSocialMediaIconDTO);
        Task DeleteAsync(int socialMediaId);
    }
}