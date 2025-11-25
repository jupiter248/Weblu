using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        Task<IReadOnlyList<Article>> GetAllArticleAsync(ArticleParameters articleParameters);
        Task<Article?> GetArticleByIdAsync(int articleId);
        Task AddArticleAsync(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(Article article);
    }
}