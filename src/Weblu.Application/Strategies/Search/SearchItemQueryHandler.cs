using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Search;

namespace Weblu.Application.Strategies.Search
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