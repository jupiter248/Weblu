using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Tags;

namespace Weblu.Application.Strategies.Common.Tags
{
    public class FilterByArticleIdStrategy : ITagQueryStrategy 
    {
        public IQueryable<Tag> Query(IQueryable<Tag> tags, TagParameters tagParameters)
        {
            return tags.Where(t => t.Articles.Any(t => t.Id == tagParameters.FilterByArticleId));
        }
    }
}