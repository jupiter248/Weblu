using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Tags;

namespace Weblu.Application.Interfaces.Strategies.Common
{
    public interface ITagQueryStrategy
    {
        IQueryable<Tag> Query(IQueryable<Tag> tags, TagParameters tagParameters);
    }
}