using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Application.Strategies.Comments;
using Weblu.Domain.Entities.Common;
using Weblu.Infrastructure.Data;
using Weblu.Domain.Entities.Comments;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Weblu.Infrastructure.Repositories
{
    internal class CommentRepository : GenericRepository<Comment, CommentParameters>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<Comment>> GetAllAsync(CommentParameters commentParameters)
        {
            IQueryable<Comment> comments = _context.Comments.AsNoTracking();
            comments = new CommentQueryHandler(new FilterByArticleIdStrategy())
            .ExecuteCommentQuery(comments, commentParameters);

            if (commentParameters.CreatedDateSort != CreatedDateSort.All)
            {
                comments = new CommentQueryHandler(new CreatedDateSortStrategy())
                .ExecuteCommentQuery(comments, commentParameters);
            }
            if (!string.IsNullOrEmpty(commentParameters.UserId))
            {
                comments = new CommentQueryHandler(new FilterByUserIdStrategy())
                .ExecuteCommentQuery(comments, commentParameters);
            }
            if (commentParameters.ParentCommentId.HasValue)
            {
                comments = new CommentQueryHandler(new FilterByParentCommentIdStrategy())
                .ExecuteCommentQuery(comments, commentParameters);
            }

            return await PaginationExtensions<Comment>.GetPagedList(comments, commentParameters.PageNumber, commentParameters.PageSize);
        }

        public async Task<int> GetCountAsync(int articleId)
        {
            int count = await _context.Comments.CountAsync(c => c.ArticleId == articleId);
            return count;
        }

        public async Task<Dictionary<int, int>> GetCountByIdsAsync(List<int> articleIds)
        {
            return await _context.Comments.Where(c => articleIds.Contains(c.ArticleId)).GroupBy(c => c.ArticleId)
            .Select(c => new
            {
                ArticleId = c.Key,
                Count = c.Count()
            })
            .ToDictionaryAsync(x => x.ArticleId, x => x.Count);
        }
    }
}