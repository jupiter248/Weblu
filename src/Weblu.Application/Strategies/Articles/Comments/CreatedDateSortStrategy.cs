using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles.Comments;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Articles.Comments
{
    public class CreatedDateSortStrategy : ICommentQueryStrategy
    {
        public IQueryable<Comment> Query(IQueryable<Comment> comments, CommentParameters commentParameters)
        {
            if (commentParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return comments.OrderByDescending(c => c.CreatedAt);
            }
            else if (commentParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return comments.OrderBy(c => c.CreatedAt);
            }
            else
            {
                return comments;
            }
        }
    }
}