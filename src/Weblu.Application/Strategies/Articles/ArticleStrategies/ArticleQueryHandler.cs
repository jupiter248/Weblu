using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Strategies.Articles.ArticleStrategies
{
    public class ArticleQueryHandler
    {
        private readonly IArticleQueryStrategy _articleQueryStrategy;
        public ArticleQueryHandler(IArticleQueryStrategy articleQueryStrategy)
        {
            _articleQueryStrategy = articleQueryStrategy;
        }

        public IQueryable<Article> ExecuteArticleQuery(IQueryable<Article> articles , ArticleParameters articleParameters)
        {
            return _articleQueryStrategy.Query(articles , articleParameters);
        }
    }
}