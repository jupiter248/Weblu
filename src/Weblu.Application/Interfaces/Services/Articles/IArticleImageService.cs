using Weblu.Application.DTOs.Articles.ArticleDTOs.ArticleImageDTOs;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleImageService
    {
        Task AddAsync(int articleId, int imageId, AddArticleImageDTO addArticleImageDTO);
        Task DeleteAsync(int articleId, int imageId);
    }
}