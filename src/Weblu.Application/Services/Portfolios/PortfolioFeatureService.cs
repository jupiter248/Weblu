using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services.Portfolios
{
    public class PortfolioFeatureService : IPortfolioFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFeatureRepository _featureRepository;
        public PortfolioFeatureService(
            IUnitOfWork unitOfWork,
            IPortfolioRepository portfolioRepository,
            IFeatureRepository featureRepository
        )
        {
            _featureRepository = featureRepository;
            _unitOfWork = unitOfWork;
            _portfolioRepository = portfolioRepository;
        }
        public async Task AddFeatureAsync(int portfolioId, int featureId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            await _portfolioRepository.LoadFeaturesAsync(portfolio);

            portfolio.AddFeature(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFeatureAsync(int portfolioId, int featureId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            await _portfolioRepository.LoadFeaturesAsync(portfolio);

            portfolio.DeleteFeature(feature);
            await _unitOfWork.CommitAsync();
        }
    }
}