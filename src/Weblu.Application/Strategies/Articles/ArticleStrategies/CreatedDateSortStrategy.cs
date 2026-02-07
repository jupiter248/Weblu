using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Articles.ArticleStrategies
{
    public class CreatedDateSortStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {
            if (articleParameters.CreatedDateSort == CreatedDateSort.Newest)
            {
                return articles.OrderByDescending(s => s.CreatedAt);
            }
            else if (articleParameters.CreatedDateSort == CreatedDateSort.Oldest)
            {
                return articles.OrderBy(s => s.CreatedAt);
            }
            else
            {
                return articles;
            }
        }
    }
}