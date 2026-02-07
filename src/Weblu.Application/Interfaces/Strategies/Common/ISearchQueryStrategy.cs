using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Search;

namespace Weblu.Application.Interfaces.Strategies.Common
{
    public interface ISearchQueryStrategy
    {
        IQueryable<SearchItem> Query(IQueryable<SearchItem> searchItems, SearchParameters searchParameters);

    }
}