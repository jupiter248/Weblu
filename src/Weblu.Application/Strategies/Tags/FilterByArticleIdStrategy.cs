using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Tags;

namespace Weblu.Application.Strategies.Tags
{
    public class FilterByArticleIdStrategy : ITagQueryStrategy
    {
        public IQueryable<Tag> Query(IQueryable<Tag> tags, TagParameters tagParameters)
        {
            return tags.Where(t => t.Articles.Any(t => t.Id == tagParameters.FilterByArticleId));
        }
    }
}