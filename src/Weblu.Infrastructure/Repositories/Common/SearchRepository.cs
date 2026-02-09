using Microsoft.EntityFrameworkCore;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Strategies.Common.Search;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories.Common
{
    public class SearchRepository : ISearchRepository
    {
        public readonly ApplicationDbContext _context;
        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(SearchItem searchItem)
        {
            _context.SearchItems.Remove(searchItem);
        }

        public async Task<SearchItem?> GetByEntityIdAsync(int entityId, SearchEntityType entityType)
        {
            return await _context.SearchItems.FirstOrDefaultAsync(s => s.EntityId == entityId && s.EntityType == entityType);
        }

        public async Task IndexAsync(SearchItem searchItems)
        {
            await _context.SearchItems.AddAsync(searchItems);
        }

        public async Task<PagedList<SearchItem>> SearchAsync(string text, SearchParameters searchParameters)
        {
            var items = _context.SearchItems.Where(s => s.Title.ToLower().Contains(text.ToLower()) || s.Content.ToLower().Contains(text.ToLower())).AsQueryable();

            if (searchParameters.SearchEntityType != SearchEntityType.All)
            {
                items = new SearchItemQueryHandler(new FilterBySearchEntityTypeStrategy())
                .ExecuteSearchQuery(items, searchParameters);
            }
            if (searchParameters.SearchEntityType != SearchEntityType.All)
            {
                items = new SearchItemQueryHandler(new IndexedDateSortStrategy())
                .ExecuteSearchQuery(items, searchParameters);
            }

            return await PaginationExtensions<SearchItem>.GetPagedList(items, searchParameters.PageNumber, searchParameters.PageSize);
        }

        public void Update(SearchItem searchItem)
        {
            _context.SearchItems.Update(searchItem);
        }
    }
}