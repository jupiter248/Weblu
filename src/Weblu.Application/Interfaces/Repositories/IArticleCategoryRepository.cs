using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IArticleCategoryRepository
    {
        Task<IReadOnlyList<ArticleCategory>> GetAllArticleCategoriesAsync();
        Task<ArticleCategory?> GetArticleCategoryByIdAsync(int categoryId);
        Task AddArticleCategoryAsync(ArticleCategory articleCategory);
        void UpdateArticleCategory(ArticleCategory articleCategory);
        void DeleteArticleCategory(ArticleCategory articleCategory);
    }
}