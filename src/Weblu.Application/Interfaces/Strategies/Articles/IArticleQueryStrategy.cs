using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Interfaces.Strategies.Articles
{
    public interface IArticleQueryStrategy
    {
        IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters);
    }
}