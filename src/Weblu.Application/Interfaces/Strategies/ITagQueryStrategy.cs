using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Tags;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface ITagQueryStrategy
    {
        IQueryable<Tag> Query(IQueryable<Tag> tags, TagParameters tagParameters);
    }
}