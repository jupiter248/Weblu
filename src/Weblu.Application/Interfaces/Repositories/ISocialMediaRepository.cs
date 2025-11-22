using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ISocialMediaRepository
    {
        Task<IReadOnlyList<SocialMedia>> GetAllSocialMediasAsync();
        Task<SocialMedia?> GetSocialMediaByIdAsync(int socialMediaId);
        Task AddSocialMediaAsync(SocialMedia socialMedia);
        void UpdateSocialMedia(SocialMedia socialMedia);
        void DeleteSocialMedia(SocialMedia socialMedia);
    }
}