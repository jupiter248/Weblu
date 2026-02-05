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
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;

namespace Weblu.Infrastructure.Repositories
{
    internal class ArticleRepository : GenericRepository<Article, ArticleParameters>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override async Task<PagedList<Article>> GetAllAsync(ArticleParameters articleParameters)
        {
            IQueryable<Article> articles = _context.Articles.Where(a => !a.IsDeleted).Include(a => a.ArticleImages.Where(i => i.IsThumbnail)).ThenInclude(i => i.Image).AsNoTracking();

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
                .ExecuteArticleQuery(articles.Include(c => c.Contributors), articleParameters);
            }
            if (articleParameters.ViewCountSort != ViewCountSort.All)
            {
                articles = new ArticleQueryHandler(new ViewCountSortStrategy())
                .ExecuteArticleQuery(articles, articleParameters);
            }
            if (articleParameters.LikeCountSort != LikeCountSort.All)
            {
                articles = new ArticleQueryHandler(new LikeCountSortStrategy())
                .ExecuteArticleQuery(articles.Include(l => l.ArticleLikes), articleParameters);
            }
            if (articleParameters.CommentCountSort != CommentCountSort.All)
            {
                articles = new ArticleQueryHandler(new CommentCountSortStrategy())
                .ExecuteArticleQuery(articles.Include(c => c.Comments), articleParameters);
            }
            var pagedList = await PaginationExtensions<Article>.GetPagedList(articles, articleParameters.PageNumber, articleParameters.PageSize);
            return pagedList;

        }

        public override async Task<Article?> GetByIdAsync(int articleId)
        {
            Article? article = await _context.Articles.Where(a => !a.IsDeleted).Include(c => c.Category).FirstOrDefaultAsync(a => a.Id == articleId);
            return article;
        }

        public async Task<int> GetLikeCountAsync(int articleId)
        {
            int count = await _context.ArticleLikes.CountAsync(l => l.Id == articleId);
            return count;
        }

        public async Task<Article?> GetByIdWithImagesAsync(int articleId)
        {
            Article? article = await _context.Articles.Include(c => c.Category).Include(c => c.ArticleImages).ThenInclude(a => a.Image).FirstOrDefaultAsync(a => a.Id == articleId);
            return article;
        }

        public async Task LoadContributorsAsync(Article article)
        {
            await _context.Entry(article).Collection(c => c.Contributors).LoadAsync();
        }

        public async Task LoadLikesAsync(Article article)
        {
            await _context.Entry(article).Collection(c => c.ArticleLikes).LoadAsync();
        }

        public async Task LoadTagsAsync(Article article)
        {
            await _context.Entry(article).Collection(c => c.Tags).LoadAsync();
        }

        public async Task<Dictionary<int, int>> GetLikeCountByIdsAsync(List<int> ids)
        {
            return await _context.ArticleLikes.Where(a => !a.IsDeleted).Where(l => ids.Contains(l.ArticleId)).GroupBy(l => l.ArticleId)
            .Select(l => new
            {
                ArticleId = l.Key,
                Count = l.Count(),
            })
            .ToDictionaryAsync(x => x.ArticleId, x => x.Count);
        }

        public async Task<IEnumerable<Article>> GetByTitleAsync(string title)
        {
            return await _context.Articles.Where(a => !a.IsDeleted).Where(a => a.Title.ToLower().Contains(title.ToLower())).ToListAsync();
        }
    }
}