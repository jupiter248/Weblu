using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Enums.Articles.Parameters;

namespace Weblu.Application.Strategies.Articles
{
    public class LikeCountSortStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {
            if (articleParameters.LikeCountSort == LikeCountSort.TheMost)
            {
                return articles.OrderByDescending(s => s.ArticleLikes.Count());
            }
            else if (articleParameters.ViewCountSort == ViewCountSort.TheLeast)
            {
                return articles.OrderBy(s => s.ArticleLikes.Count());
            }
            else
            {
                return articles;
            }
        }
    }
}