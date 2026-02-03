using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Strategies.Search
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