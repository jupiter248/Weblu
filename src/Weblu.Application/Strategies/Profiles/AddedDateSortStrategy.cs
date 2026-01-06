using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Profiles
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