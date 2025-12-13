using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Profiles;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class ProfileImageRepository : GenericRepository<ProfileMedia, ProfileMediaParameters>, IProfileImageRepository
    {
        public ProfileImageRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IReadOnlyList<ProfileMedia>> GetAllAsync(ProfileMediaParameters profileMediaParameters)
        {
            IQueryable<ProfileMedia> profiles = _context.ProfileMedia.AsNoTracking();
            if (profileMediaParameters.AddedDateSort != CreatedDateSort.All)
            {
                profiles = new ProfileMediaQueryHandler(new AddedDateSortStrategy())
                .ExecuteProfileQuery(profiles, profileMediaParameters);
            }

            return await profiles.ToListAsync();
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