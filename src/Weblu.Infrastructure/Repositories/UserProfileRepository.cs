using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Profiles;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class ProfileImageRepository : IProfileImageRepository
    {
        public readonly ApplicationDbContext _context;
        public ProfileImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddProfileAsync(ProfileMedia profile)
        {
            await _context.ProfileMedia.AddAsync(profile);
        }

        public void DeleteProfile(ProfileMedia profile)
        {
            _context.ProfileMedia.Remove(profile);
        }

        public async Task<List<ProfileMedia>> GetAllProfilesAsync(ProfileMediaParameters profileMediaParameters)
        {
            List<ProfileMedia> profiles = await _context.ProfileMedia.ToListAsync();

            var addedDateSort = new ProfileMediaQueryHandler(new AddedDateSortStrategy());
            profiles = addedDateSort.ExecuteServiceQuery(profiles, profileMediaParameters);

            return profiles;
        }

        public async Task<ProfileMedia?> GetProfileByIdAsync(int profileId)
        {
            ProfileMedia? profile = await _context.ProfileMedia.FirstOrDefaultAsync(i => i.Id == profileId);
            if (profile == null)
            {
                return null;
            }
            return profile;
        }

        public async Task<bool> ProfileExistsAsync(int profileId)
        {
            bool profileMediaExists = await _context.ProfileMedia.AnyAsync(i => i.Id == profileId);

            return profileMediaExists;
        }

        public void UpdateProfile(ProfileMedia profile)
        {
            _context.ProfileMedia.Update(profile);
        }

        public async Task<bool> UserHasMainProfileAsync(string userId)
        {
            bool userHasMainProfile = await _context.ProfileMedia.AnyAsync(u => u.OwnerId == userId && u.IsMain && u.OwnerType == ProfileMediaType.User);
            if (!userHasMainProfile)
            {
                return false;
            }
            return true;
        }
    }
}