using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Domain.Interfaces.Repositories.Articles
{
    public interface IArticleCategoryRepository : IGenericRepository<ArticleCategory, ArticleCategoryParameters>
    {
    }
}