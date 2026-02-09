using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services.Portfolios
{
    public class PortfolioContributorService : IPortfolioContributorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IContributorRepository _contributorRepository;
        public PortfolioContributorService(
        IUnitOfWork unitOfWork,

        IPortfolioRepository portfolioRepository,
        IContributorRepository contributorRepository
        )
        {
            _unitOfWork = unitOfWork;
            _portfolioRepository = portfolioRepository;
            _contributorRepository = contributorRepository;
        }
        public async Task AddAsync(int portfolioId, int contributorId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            await _portfolioRepository.LoadContributorsAsync(portfolio);

            portfolio.AddContributor(contributor);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int portfolioId, int contributorId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            await _portfolioRepository.LoadContributorsAsync(portfolio);

            portfolio.RemoveContributor(contributor);
            await _unitOfWork.CommitAsync();
        }
    }
}