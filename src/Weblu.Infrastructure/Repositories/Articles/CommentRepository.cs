using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Domain.Entities.Articles.Comments;
using Weblu.Application.Parameters.Articles;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Strategies.Articles.Comments;

namespace Weblu.Infrastructure.Repositories.Articles
{
    internal class CommentRepository : GenericRepository<Comment, CommentParameters>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedList<Comment>> GetAllAsync(CommentParameters commentParameters)
        {
            IQueryable<Comment> comments = _context.Comments.Where(a => !a.IsDeleted).AsNoTracking();
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
            int count = await _context.Comments.Where(a => !a.IsDeleted).CountAsync(c => c.ArticleId == articleId);
            return count;
        }

        public async Task<Dictionary<int, int>> GetCountByIdsAsync(List<int> articleIds)
        {
            return await _context.Comments.Where(a => !a.IsDeleted).Where(c => articleIds.Contains(c.ArticleId)).GroupBy(c => c.ArticleId)
            .Select(c => new
            {
                ArticleId = c.Key,
                Count = c.Count()
            })
            .ToDictionaryAsync(x => x.ArticleId, x => x.Count);
        }
    }
}