using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Enums.Articles.Parameters;

namespace Weblu.Application.Strategies.Articles
{
    public class ViewCountSortStrategy : IArticleQueryStrategy
    {
        public IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters)
        {
            if (articleParameters.ViewCountSort == ViewCountSort.TheMost)
            {
                return articles.OrderByDescending(s => s.ViewCount);
            }
            else if (articleParameters.ViewCountSort == ViewCountSort.TheLeast)
            {
                return articles.OrderBy(s => s.ViewCount);
            }
            else
            {
                return articles;
            }
        }
    }
}