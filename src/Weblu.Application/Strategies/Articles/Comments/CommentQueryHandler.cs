using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles.Comments;

namespace Weblu.Application.Strategies.Articles.Comments
{
    public class CommentQueryHandler
    {
        private readonly ICommentQueryStrategy _commentQueryStrategy;
        public CommentQueryHandler(ICommentQueryStrategy commentQueryStrategy)
        {
            _commentQueryStrategy = commentQueryStrategy;
        }
        public IQueryable<Comment> ExecuteCommentQuery(IQueryable<Comment> comments, CommentParameters commentParameters)
        {
            return _commentQueryStrategy.Query(comments, commentParameters);
        }
    }
}