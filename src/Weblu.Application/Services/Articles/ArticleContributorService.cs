using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Services.Articles
{
    public class ArticleContributorService : IArticleContributorService
    {
        private readonly IContributorRepository _contributorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleRepository _articleRepository;

        public ArticleContributorService(IContributorRepository contributorRepository, IUnitOfWork unitOfWork, IArticleRepository articleRepository)
        {
            _contributorRepository = contributorRepository;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
        }
        public async Task AddAsync(int articleId, int contributorId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            await _articleRepository.LoadContributorsAsync(article);

            article.AddContributor(contributor);
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(int articleId, int contributorId)
        {
            Article article = await _articleRepository.GetByIdAsync(articleId) ?? throw new NotFoundException(ArticleErrorCodes.NotFound);
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            await _articleRepository.LoadContributorsAsync(article);

            article.DeleteContributor(contributor);
            await _unitOfWork.CommitAsync();
        }
    }
}