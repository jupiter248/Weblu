using Weblu.Application.Interfaces.Strategies.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Strategies.Common.Search
{
    public class FilterBySearchEntityTypeStrategy : ISearchQueryStrategy
    {
        public IQueryable<SearchItem> Query(IQueryable<SearchItem> searchItems, SearchParameters searchParameters)
        {
            if (searchParameters.SearchEntityType == SearchEntityType.Portfolio)
            {
                return searchItems.Where(t => t.EntityType == SearchEntityType.Portfolio);
            }
            else if (searchParameters.SearchEntityType == SearchEntityType.Article)
            {
                return searchItems.Where(t => t.EntityType == SearchEntityType.Article);
            }
            else
            {
                return searchItems;
            }
        }
    }
}