using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Strategies.Articles
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