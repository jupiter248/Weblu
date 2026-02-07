using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles.Comments;

namespace Weblu.Application.Interfaces.Repositories.Articles
{
    public interface ICommentRepository : IGenericRepository<Comment, CommentParameters>
    {
        Task<int> GetCountAsync(int articleId);
        Task<Dictionary<int, int>> GetCountByIdsAsync(List<int> articleIds);
    }
}