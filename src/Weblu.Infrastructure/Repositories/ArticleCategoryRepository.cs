using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Articles;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddArticleCategoryAsync(ArticleCategory articleCategory)
        {
            await _context.ArticleCategories.AddAsync(articleCategory);
        }

        public void DeleteArticleCategory(ArticleCategory articleCategory)
        {
            _context.ArticleCategories.Remove(articleCategory);
        }

        public async Task<IReadOnlyList<ArticleCategory>> GetAllArticleCategoriesAsync()
        {
            IQueryable<ArticleCategory> articleCategories = _context.ArticleCategories.AsQueryable();

            return await articleCategories.ToListAsync();
        }

        public async Task<ArticleCategory?> GetArticleCategoryByIdAsync(int categoryId)
        {
            ArticleCategory? articleCategory = await _context.ArticleCategories.FirstOrDefaultAsync(c => c.Id == categoryId);
            return articleCategory;
        }

        public void UpdateArticleCategory(ArticleCategory articleCategory)
        {
            _context.ArticleCategories.Update(articleCategory);
        }
    }
}