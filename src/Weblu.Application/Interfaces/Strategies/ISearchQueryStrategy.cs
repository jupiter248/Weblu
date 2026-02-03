using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Search;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface ISearchQueryStrategy
    {
        IQueryable<SearchItem> Query(IQueryable<SearchItem> searchItems, SearchParameters searchParameters);

    }
}