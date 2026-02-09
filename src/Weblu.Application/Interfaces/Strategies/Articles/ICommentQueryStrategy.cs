using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles.Comments;

namespace Weblu.Application.Interfaces.Strategies.Articles
{
    public interface ICommentQueryStrategy
    {
        IQueryable<Comment> Query(IQueryable<Comment> comments, CommentParameters commentParameters);
    }
}