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
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class TagRepository : GenericRepository<Tag, TagParameters>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IReadOnlyList<Tag>> GetAllAsync(TagParameters tagParameters)
        {
            IQueryable<Tag> tags = _context.Tags.AsNoTracking();
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
            return await tags.ToListAsync();
        }
    }
}