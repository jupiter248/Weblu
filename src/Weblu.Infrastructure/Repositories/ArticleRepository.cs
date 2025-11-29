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

            var createdDateSort = new ArticleQueryHandler(new CreatedDateSortStrategy());
            articles = createdDateSort.ExecuteArticleQuery(articles, articleParameters);

            var filteredByCategoryId = new ArticleQueryHandler(new FilteredByCategoryIdStrategy());
            articles = filteredByCategoryId.ExecuteArticleQuery(articles, articleParameters);

            var filteredByContributorId = new ArticleQueryHandler(new FilteredByContributorIdStrategy());
            articles = filteredByContributorId.ExecuteArticleQuery(articles, articleParameters);

            var ViewCountSort = new ArticleQueryHandler(new ViewCountSortStrategy());
            articles = ViewCountSort.ExecuteArticleQuery(articles, articleParameters);

            var LikeCountSort = new ArticleQueryHandler(new LikeCountSortStrategy());
            articles = LikeCountSort.ExecuteArticleQuery(articles, articleParameters);

            var CommentCountSort = new ArticleQueryHandler(new CommentCountSortStrategy());
            articles = CommentCountSort.ExecuteArticleQuery(articles, articleParameters);

            return await articles.ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int articleId)
        {
            Article? article = await _context.Articles.Include(c => c.Category).Include(c => c.Contributors).Include(c => c.ArticleImages).ThenInclude(a => a.Image).FirstOrDefaultAsync(a => a.Id == articleId);
            return article;
        }

        public void UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
        }
    }
}