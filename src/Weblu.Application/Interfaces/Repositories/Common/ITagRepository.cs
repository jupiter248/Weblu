using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Tags;

namespace Weblu.Application.Interfaces.Repositories.Common
{
    public interface ITagRepository : IGenericRepository<Tag, TagParameters>
    {
    }
}