using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IArticleService
    {
        Task<List<ArticleSummaryDto>> GetAllArticlesAsync(ArticleParameters articleParameters);
        Task<ArticleDetailDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDetailDto> AddArticleAsync(AddArticleDto addArticleDto);
        Task<ArticleDetailDto> UpdateArticleAsync(int articleId, UpdateArticleDto updateArticleDto);
        Task DeleteArticleAsync(int articleId);
        // Images
        Task AddImageToArticleAsync(int articleId, int imageId, AddArticleImageDto addArticleImageDto);
        Task DeleteImageFromArticleAsync(int articleId, int imageId);
        // Contributors
        Task AddContributorToArticleAsync(int articleId, int contributorId);
        Task DeleteContributorFromArticleAsync(int articleId, int contributorId);
        // Likes
        Task LikeArticleAsync(int articleId, string userId);
        Task UnlikeArticleAsync(int articleId, string userId);


    }
}