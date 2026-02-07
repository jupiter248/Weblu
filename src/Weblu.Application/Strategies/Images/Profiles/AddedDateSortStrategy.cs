using Weblu.Application.Interfaces.Strategies.Images;
using Weblu.Application.Parameters.Images;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Images.Profiles
{
    public class AddedDateSortStrategy : IProfileQueryStrategy
    {
        public IQueryable<ProfileMedia> Query(IQueryable<ProfileMedia> profiles, ProfileMediaParameters profileMediaParameters)
        {
            if (profileMediaParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return profiles.OrderByDescending(s => s.AddedAt);
            }
            else if (profileMediaParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return profiles.OrderBy(s => s.AddedAt);
            }
            else
            {
                return profiles;
            }
        }
    }
}