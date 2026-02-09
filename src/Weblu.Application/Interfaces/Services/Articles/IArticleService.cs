using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Articles.ArticleDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleService
    {
        Task<List<ArticleSummaryDTO>> GetAllAsync(ArticleParameters articleParameters);
        Task<PagedResponse<ArticleSummaryDTO>> GetAllPagedAsync(ArticleParameters articleParameters);
        Task<ArticleDetailDTO> GetByIdAsync(int articleId);
        Task<ArticleDetailDTO> CreateAsync(CreateArticleDTO createArticleDTO);
        Task<ArticleDetailDTO> EditAsync(int articleId, UpdateArticleDTO updateArticleDTO);
        Task DeleteAsync(int articleId);
        Task ViewAsync(int articleId);
        Task Publish(int articleId);
        Task Unpublish(int articleId);

    }
}