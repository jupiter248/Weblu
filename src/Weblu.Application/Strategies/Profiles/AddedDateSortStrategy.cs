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
        public List<ProfileMedia> Query(List<ProfileMedia> profiles, ProfileMediaParameters profileMediaParameters)
        {
            if (profileMediaParameters.AddedDateSort == CreatedDateSort.Newest)
            {
                return profiles.OrderByDescending(s => s.AddedAt).ToList();
            }
            else if (profileMediaParameters.AddedDateSort == CreatedDateSort.Oldest)
            {
                return profiles.OrderBy(s => s.AddedAt).ToList();
            }
            else
            {
                return profiles;
            }
        }
    }
}