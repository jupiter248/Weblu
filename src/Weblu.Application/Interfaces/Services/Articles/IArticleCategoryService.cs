using Weblu.Application.Dtos.Articles.ArticleCategoryDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleCategoryService
    {
        Task<List<ArticleCategoryDto>> GetAllAsync(ArticleCategoryParameters articleCategoryParameters);
        Task<ArticleCategoryDto> GetByIdAsync(int categoryId);
        Task<ArticleCategoryDto> CreateAsync(CreateArticleCategoryDto createArticleCategoryDto);
        Task<ArticleCategoryDto> UpdateAsync(int categoryId, UpdateArticleCategoryDto updateArticleCategoryDto);
        Task DeleteAsync(int categoryId);
    }
}