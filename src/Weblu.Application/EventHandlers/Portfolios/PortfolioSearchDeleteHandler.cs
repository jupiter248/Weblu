using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Search;
using Weblu.Domain.Events.Portfolios;
using Weblu.Domain.Interfaces;

namespace Weblu.Application.EventHandlers.Portfolios
{
    public class PortfolioSearchDeleteHandler : IDomainEventHandler<PortfolioDeletedEvent>
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PortfolioSearchDeleteHandler(ISearchRepository searchRepository, IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork)
        {
            _searchRepository = searchRepository;
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(PortfolioDeletedEvent domainEvent)
        {
            Portfolio? portfolio = await _portfolioRepository.GetByPublicIdAsync(domainEvent.PortfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            SearchItem? searchItem = await _searchRepository.GetByEntityIdAsync(portfolio.Id, SearchEntityType.Portfolio) ?? throw new NotFoundException(SearchErrorCodes.NotFound);
            _searchRepository.Delete(searchItem);
            await _unitOfWork.CommitAsync();
        }
    }
}