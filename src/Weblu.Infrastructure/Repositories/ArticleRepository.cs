using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Enums.Articles.Parameters;
using Weblu.Domain.Enums.Common.Parameters;
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
            IQueryable<Article> articles = _context.Articles.Include(l => l.ArticleLikes).Include(c => c.Category).Include(c => c.Comments);

            if (articleParameters.CreatedDateSort != CreatedDateSort.All)
            {
                articles = new ArticleQueryHandler(new CreatedDateSortStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            if (articleParameters.CategoryId.HasValue)
            {
                articles = new ArticleQueryHandler(new FilteredByCategoryIdStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            if (articleParameters.ContributorId.HasValue)
            {
                articles = new ArticleQueryHandler(new FilteredByContributorIdStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            if (articleParameters.ViewCountSort != ViewCountSort.All)
            {
                articles = new ArticleQueryHandler(new ViewCountSortStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            if (articleParameters.LikeCountSort != LikeCountSort.All)
            {
                articles = new ArticleQueryHandler(new LikeCountSortStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            if (articleParameters.CommentCountSort != CommentCountSort.All)
            {
                articles = new ArticleQueryHandler(new CommentCountSortStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            
            return await articles.ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int articleId)
        {
            Article? article = await _context.Articles.Include(t => t.Tags).Include(l => l.ArticleLikes).Include(c => c.Category).Include(c => c.Contributors).Include(c => c.Comments).Include(c => c.ArticleImages).ThenInclude(a => a.Image).FirstOrDefaultAsync(a => a.Id == articleId);
            return article;
        }

        public void UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
        }
    }
}