using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IProfileImageRepository
    {
        Task<IReadOnlyList<ProfileMedia>> GetAllProfilesAsync(ProfileMediaParameters profileMediaParameters);
        Task<ProfileMedia?> GetProfileByIdAsync(int profileId);
        Task<bool> ProfileExistsAsync(int profileId);
        Task<bool> UserHasMainProfileAsync(string userId);
        Task AddProfileAsync(ProfileMedia profile);
        void UpdateProfile(ProfileMedia profile);
        void DeleteProfile(ProfileMedia profile);
    }
}