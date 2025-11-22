using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.SocialMediaDtos;

namespace Weblu.Application.Interfaces.Services
{
    public interface ISocialMediaService
    {
        Task<List<SocialMediaDto>> GetAllSocialMediasAsync();
        Task<SocialMediaDto> GetSocialMediaByIdAsync(int socialMediaId);
        Task<SocialMediaDto> AddSocialMediaAsync(AddSocialMediaDto addSocialMediaDto);
        Task<SocialMediaDto> UpdateSocialMediaAsync(int socialMediaId, UpdateSocialMediaDto updateSocialMediaDto);
        Task<SocialMediaDto> UpdateHeadImageSocialMediaAsync(int socialMediaId, UpdateSocialMediaIconDto updateSocialMediaIcon);
        Task DeleteSocialMediaAsync(int socialMediaId);
    }
}