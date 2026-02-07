using Weblu.Domain.Entities.Articles;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Parameters.Articles;
using Weblu.Application.Interfaces.Repositories.Articles;

namespace Weblu.Infrastructure.Repositories.Articles
{
    internal class ArticleCategoryRepository : GenericRepository<ArticleCategory, ArticleCategoryParameters>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}