using Weblu.Application.Dtos.Articles.ArticleCategoryDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleCategoryService
    {
        Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync(ArticleCategoryParameters articleCategoryParameters);
        Task<ArticleCategoryDto> GetArticleCategoryByIdAsync(int categoryId);
        Task<ArticleCategoryDto> AddArticleCategoryAsync(AddArticleCategoryDto addArticleCategoryDto);
        Task<ArticleCategoryDto> UpdateArticleCategoryAsync(int categoryId, UpdateArticleCategoryDto updateArticleCategoryDto);
        Task DeleteArticleCategoryAsync(int categoryId);
    }
}