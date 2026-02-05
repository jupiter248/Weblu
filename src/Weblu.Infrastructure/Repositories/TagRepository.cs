using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Tags;
using Weblu.Application.Validations.Tags;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class TagRepository : GenericRepository<Tag, TagParameters>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<PagedList<Tag>> GetAllAsync(TagParameters tagParameters)
        {
            IQueryable<Tag> tags = _context.Tags.Where(a => !a.IsDeleted).AsNoTracking();
            if (tagParameters.CreatedDateSort != CreatedDateSort.All)
            {
                tags = new TagQueryHandler(new CreatedDateSortStrategy())
                .ExecuteTagQuery(tags, tagParameters);
            }
            if (tagParameters.FilterByArticleId.HasValue)
            {
                tags = new TagQueryHandler(new FilterByArticleIdStrategy())
                .ExecuteTagQuery(tags.Include(a => a.Articles), tagParameters);
            }
            return await PaginationExtensions<Tag>.GetPagedList(tags, tagParameters.PageNumber, tagParameters.PageSize);
        }
    }
}