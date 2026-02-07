using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Search;

namespace Weblu.Application.Strategies.Common.Search
{
    public class SearchItemQueryHandler
    {
        public readonly ISearchQueryStrategy _searchQueryStrategy;
        public SearchItemQueryHandler(ISearchQueryStrategy searchQueryStrategy)
        {
            _searchQueryStrategy = searchQueryStrategy;
        }
        public IQueryable<SearchItem> ExecuteSearchQuery(IQueryable<SearchItem> searchItems, SearchParameters searchParameters)
        {
            return _searchQueryStrategy.Query(searchItems, searchParameters);
        }
    }
}