using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Strategies;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Strategies.Articles
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