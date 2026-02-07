using Weblu.Application.Interfaces.Strategies.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Enums.Articles.Parameters;

namespace Weblu.Application.Strategies.Articles.ArticleStrategies
{
    public class CommentCountSortStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {
            if (articleParameters.CommentCountSort == CommentCountSort.TheMost)
            {
                return articles.OrderByDescending(s => s.Comments.Count());
            }
            else if (articleParameters.CommentCountSort == CommentCountSort.TheLeast)
            {
                return articles.OrderBy(s => s.Comments.Count());
            }
            else
            {
                return articles;
            }
        }
    }
}