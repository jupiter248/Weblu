using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ArticleCategoryDtos;

namespace Weblu.Application.Interfaces.Services
{
    public interface IArticleCategoryService
    {
        Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync();
        Task<ArticleCategoryDto> GetArticleCategoryByIdAsync(int categoryId);
        Task<ArticleCategoryDto> AddArticleCategoryAsync(AddArticleCategoryDto addArticleCategoryDto);
        Task<ArticleCategoryDto> UpdateArticleCategoryAsync(int categoryId, UpdateArticleCategoryDto updateArticleCategoryDto);
        Task DeleteArticleCategoryAsync(int categoryId);
    }
}