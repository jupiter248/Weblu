using Microsoft.EntityFrameworkCore;
using Weblu.Application.Common.Pagination;

namespace Weblu.Infrastructure.Common.Pagination
{
    public class PaginationExtensions<T>
    {
        public static async Task<PagedList<T>> GetPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;


            var count = await source.CountAsync();
            var items = await source.Order().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}