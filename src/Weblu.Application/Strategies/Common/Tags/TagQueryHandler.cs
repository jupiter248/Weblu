using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Tags;

namespace Weblu.Application.Strategies.Common.Tags
{
    public class TagQueryHandler
    {
        private readonly ITagQueryStrategy _tagQueryStrategy;
        public TagQueryHandler(ITagQueryStrategy tagQueryStrategy)
        {
            _tagQueryStrategy = tagQueryStrategy;
        }
        public IQueryable<Tag> ExecuteTagQuery(IQueryable<Tag> tags, TagParameters tagParameters)
        {
            return _tagQueryStrategy.Query(tags, tagParameters);
        }
    }
}