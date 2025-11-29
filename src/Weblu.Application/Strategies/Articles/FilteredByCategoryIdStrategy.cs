using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Strategies.Articles
{
    public class FilteredByCategoryIdStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {
            if (articleParameters.CategoryId.HasValue)
            {
                return articles.Where(p => p.CategoryId == articleParameters.CategoryId);
            }
            else
            {
                return articles;
            }
        }
    }
}