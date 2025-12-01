using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Comments;
using Weblu.Domain.Entities.Common;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public async Task<IReadOnlyList<Comment>> GetAllCommentsAsync(CommentParameters commentParameters)
        {
            IQueryable<Comment> comments = _context.Comments.AsQueryable();

            var createdDateSort = new CommentQueryHandler(new CreatedDateSortStrategy());
            comments = createdDateSort.ExecuteCommentQuery(comments, commentParameters);

            var filterByArticleId = new CommentQueryHandler(new FilterByArticleIdStrategy());
            comments = filterByArticleId.ExecuteCommentQuery(comments, commentParameters);

            var filterByUserId = new CommentQueryHandler(new FilterByUserIdStrategy());
            comments = filterByUserId.ExecuteCommentQuery(comments, commentParameters);

            var filterByCommentParentId = new CommentQueryHandler(new FilterByParentCommentIdStrategy());
            comments = filterByCommentParentId.ExecuteCommentQuery(comments, commentParameters);

            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            return comment;
        }

        public void UpdateComment(Comment comment)
        {
            _context.Update(comment);
        }
    }
}