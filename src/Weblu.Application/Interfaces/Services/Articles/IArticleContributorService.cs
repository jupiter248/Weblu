using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleContributorService
    {
        Task AddContributorAsync(int articleId, int contributorId);
        Task DeleteContributorAsync(int articleId, int contributorId);
    }
}