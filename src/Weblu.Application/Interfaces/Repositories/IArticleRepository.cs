using Weblu.Application.Common.Interfaces;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IArticleRepository : IGenericRepository<Article, ArticleParameters>
    {
        Task<int> GetLikeCountAsync(int articleId);
        Task LoadContributorsAsync(Article article);
        Task LoadTagsAsync(Article article);
        Task LoadLikesAsync(Article article);
        Task<Article?> GetByIdWithImagesAsync(int articleId);
    }
}