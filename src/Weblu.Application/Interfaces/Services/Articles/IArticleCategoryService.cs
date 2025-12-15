using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ArticleCategoryDtos;
using Weblu.Application.Parameters;

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