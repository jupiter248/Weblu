using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Weblu.Application.Common.Pagination
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> currentPage, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalCount = count;
            PageSize = pageSize;
            AddRange(currentPage);
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool IsPreviousPageExists => CurrentPage > 1;
        public bool IsNextPageExists => CurrentPage < TotalPages;

        public static async Task<PagedList<T>> GetPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;


            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}