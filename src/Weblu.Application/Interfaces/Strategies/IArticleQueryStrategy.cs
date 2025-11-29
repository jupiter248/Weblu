using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Interfaces.Strategies
{
    public interface IArticleQueryStrategy
    {
        IQueryable<Article> Query(IQueryable<Article> articles, ArticleParameters articleParameters);
    }
}