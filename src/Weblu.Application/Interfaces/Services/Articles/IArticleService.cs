using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Parameters;

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