using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public void DeleteArticle(Article article)
        {
            _context.Articles.Remove(article);
        }

        public async Task<IReadOnlyList<Article>> GetAllArticleAsync(ArticleParameters articleParameters)
        {
            IQueryable<Article> articles = _context.Articles.Include(c => c.Category).AsQueryable();

            return await articles.ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int articleId)
        {
            Article? article = await _context.Articles.Include(c => c.Category).FirstOrDefaultAsync(a => a.Id == articleId);
            return article;
        }

        public void UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
        }
    }
}