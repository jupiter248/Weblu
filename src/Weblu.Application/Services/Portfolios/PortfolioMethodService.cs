using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services.Portfolios
{
    public class PortfolioMethodService : IPortfolioMethodService
    {
        private readonly IMethodRepository _methodRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PortfolioMethodService(IPortfolioRepository portfolioRepository, IMethodRepository methodRepository, IUnitOfWork unitOfWork)
        {
            _methodRepository = methodRepository;
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddMethodAsync(int portfolioId, int methodId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Method method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            await _portfolioRepository.LoadMethodsAsync(portfolio);

            portfolio.AddMethod(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodAsync(int portfolioId, int methodId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Method method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            await _portfolioRepository.LoadMethodsAsync(portfolio);

            portfolio.DeleteMethod(method);
            await _unitOfWork.CommitAsync();
        }
    }
}