using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Strategies.Tags
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