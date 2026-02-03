using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface ISearchRepository
    {
        public Task IndexAsync(SearchItem searchItems);
        public Task<SearchItem?> GetByEntityIdAsync(int entityId, SearchEntityType entityType);
        public void Delete(SearchItem searchItem);
        public void Update(SearchItem searchItem);
        public Task<PagedList<SearchItem>> SearchAsync(string text, SearchParameters searchParameters);
    }
}