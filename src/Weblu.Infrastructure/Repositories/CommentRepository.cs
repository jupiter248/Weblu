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

namespace Weblu.Infrastructure.Repositories
{
    internal class CommentRepository : GenericRepository<Comment, CommentParameters>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<Comment>> GetAllAsync(CommentParameters commentParameters)
        {
            IQueryable<Comment> comments = _context.Comments.AsNoTracking();

            if (commentParameters.CreatedDateSort != CreatedDateSort.All)
            {
                comments = new CommentQueryHandler(new CreatedDateSortStrategy())
                .ExecuteCommentQuery(comments, commentParameters);
            }
            if (commentParameters.ArticleId.HasValue)
            {
                comments = new CommentQueryHandler(new FilterByArticleIdStrategy())
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

            return await comments.ToListAsync();
        }

        public async Task<int> GetCountAsync(int articleId)
        {
            int count = await _context.Comments.CountAsync(c => c.ArticleId == articleId);
            return count;
        }
    }
}