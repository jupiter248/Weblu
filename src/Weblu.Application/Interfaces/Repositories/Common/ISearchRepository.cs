using Weblu.Application.Common.Pagination;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Enums.Common.Search;

namespace Weblu.Application.Interfaces.Repositories.Common
{
    public interface ISearchRepository
    {
        Task IndexAsync(SearchItem searchItems);
        Task<SearchItem?> GetByEntityIdAsync(int entityId, SearchEntityType entityType);
        void Delete(SearchItem searchItem);
        void Update(SearchItem searchItem);
        Task<PagedList<SearchItem>> SearchAsync(string text, SearchParameters searchParameters);
    }
}