using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Strategies.Articles.ArticleStrategies
{
    public class FilteredByContributorIdStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {
                return articles.Where(p => p.Contributors.Any(c => c.Id == articleParameters.ContributorId));

        }
    }
}