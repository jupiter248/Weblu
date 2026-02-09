using Weblu.Application.Dtos.Articles.ArticleDtos.ArticleImageDtos;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleImageService
    {
        Task AddAsync(int articleId, int imageId, AddArticleImageDto addArticleImageDto);
        Task DeleteAsync(int articleId, int imageId);
    }
}