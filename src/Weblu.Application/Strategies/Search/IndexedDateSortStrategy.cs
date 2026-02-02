using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Search
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