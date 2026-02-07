using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Common.Tags
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