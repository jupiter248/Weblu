using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class ArticleCategoryRepository : GenericRepository<ArticleCategory, ArticleCategoryParameters>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}