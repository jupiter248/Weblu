using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Application.Parameters.Images;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Strategies.Images.Profiles;

namespace Weblu.Infrastructure.Repositories.Images
{
    internal class ProfileImageRepository : GenericRepository<ProfileMedia, ProfileMediaParameters>, IProfileImageRepository
    {
        public ProfileImageRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<ProfileMedia>> GetAllAsync(ProfileMediaParameters profileMediaParameters)
        {
            IQueryable<ProfileMedia> profiles = _context.ProfileMedia.AsNoTracking();
            if (profileMediaParameters.CreatedDateSort != CreatedDateSort.All)
            {
                profiles = new ProfileMediaQueryHandler(new AddedDateSortStrategy())
                .ExecuteProfileQuery(profiles, profileMediaParameters);
            }
            return await PaginationExtensions<ProfileMedia>.GetPagedList(profiles, profileMediaParameters.PageNumber, profileMediaParameters.PageSize);
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