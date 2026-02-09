using Weblu.Application.DTOs.Articles.ArticleCategoryDTOs;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleCategoryService
    {
        Task<List<ArticleCategoryDTO>> GetAllAsync(ArticleCategoryParameters articleCategoryParameters);
        Task<ArticleCategoryDTO> GetByIdAsync(int categoryId);
        Task<ArticleCategoryDTO> CreateAsync(CreateArticleCategoryDTO createArticleCategoryDTO);
        Task<ArticleCategoryDTO> UpdateAsync(int categoryId, UpdateArticleCategoryDTO updateArticleCategoryDTO);
        Task DeleteAsync(int categoryId);
    }
}