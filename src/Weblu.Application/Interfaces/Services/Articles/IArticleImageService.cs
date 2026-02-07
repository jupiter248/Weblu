using Weblu.Application.Dtos.Articles.ArticleDtos.ArticleImageDtos;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleImageService
    {
        Task AddImageAsync(int articleId, int imageId, AddArticleImageDto addArticleImageDto);
        Task DeleteImageAsync(int articleId, int imageId);
    }
}