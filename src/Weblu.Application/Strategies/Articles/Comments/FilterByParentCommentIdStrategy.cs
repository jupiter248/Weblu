using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Interfaces.Strategies.Users;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles.Comments;

namespace Weblu.Application.Strategies.Articles.Comments
{
    public class FilterByParentCommentIdStrategy : ICommentQueryStrategy
    {
        public IQueryable<Comment> Query(IQueryable<Comment> comments, CommentParameters commentParameters)
        {
            return comments.Where(c => c.ParentCommentId == commentParameters.ParentCommentId);
        }
    }
}