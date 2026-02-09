using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Common.Search
{
    public class IndexedDateSortStrategy : ISearchQueryStrategy
    {
        public IQueryable<SearchItem> Query(IQueryable<SearchItem> searchItems, SearchParameters searchParameters)
        {
            if (searchParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return searchItems.OrderByDescending(s => s.IndexedAt);
            }
            else if (searchParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return searchItems.OrderBy(s => s.IndexedAt);
            }
            else
            {
                return searchItems;
            }
        }
    }
}