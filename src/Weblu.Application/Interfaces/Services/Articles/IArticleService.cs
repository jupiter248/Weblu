using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Articles.ArticleDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleService
    {
        Task<List<ArticleSummaryDto>> GetAllArticlesAsync(ArticleParameters articleParameters);
        Task<PagedResponse<ArticleSummaryDto>> GetAllPagedArticlesAsync(ArticleParameters articleParameters);
        Task<ArticleDetailDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDetailDto> AddArticleAsync(AddArticleDto addArticleDto);
        Task<ArticleDetailDto> UpdateArticleAsync(int articleId, UpdateArticleDto updateArticleDto);
        Task DeleteArticleAsync(int articleId);
        Task ViewArticleAsync(int articleId);

    }
}