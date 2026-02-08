using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.ArticleDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleService
    {
        Task<List<ArticleSummaryDto>> GetAllAsync(ArticleParameters articleParameters);
        Task<PagedResponse<ArticleSummaryDto>> GetAllPagedAsync(ArticleParameters articleParameters);
        Task<ArticleDetailDto> GetByIdAsync(int articleId);
        Task<ArticleDetailDto> CreateAsync(CreateArticleDto createArticleDto);
        Task<ArticleDetailDto> EditAsync(int articleId, UpdateArticleDto updateArticleDto);
        Task DeleteAsync(int articleId);
        Task ViewAsync(int articleId);

    }
}