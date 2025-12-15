using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Tags
{
    public class CreatedDateSortStrategy : ITagQueryStrategy
    {
        public IQueryable<Tag> Query(IQueryable<Tag> tags, TagParameters tagParameters)
        {
            if (tagParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return tags.OrderByDescending(s => s.CreatedAt);
            }
            else if (tagParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return tags.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return tags;
            }
        }
    }
}