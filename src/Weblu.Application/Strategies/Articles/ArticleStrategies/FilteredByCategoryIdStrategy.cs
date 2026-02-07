using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Strategies.Articles.ArticleStrategies
{
    public class FilteredByCategoryIdStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {

                return articles.Where(p => p.CategoryId == articleParameters.CategoryId);
        }
    }
}