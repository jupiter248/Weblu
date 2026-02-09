using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Common.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Events.Portfolios;
using Weblu.Domain.Interfaces;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Repositories;

namespace Weblu.Application.EventHandlers.Portfolios
{
    public class PortfolioSearchUpdateHandler : IDomainEventHandler<PortfolioUpdatedEvent>
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PortfolioSearchUpdateHandler(ISearchRepository searchRepository, IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork)
        {
            _searchRepository = searchRepository;
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(PortfolioUpdatedEvent domainEvent)
        {
            Portfolio? portfolio = await _portfolioRepository.GetByGuidIdAsync(domainEvent.PortfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            SearchItem? searchItem = await _searchRepository.GetByEntityIdAsync(portfolio.Id, SearchEntityType.Portfolio) ?? throw new NotFoundException(SearchErrorCodes.NotFound);
            searchItem.Title = portfolio.Title;
            searchItem.Content = portfolio.Description;
            _searchRepository.Update(searchItem);
            await _unitOfWork.CommitAsync();
        }
    }
}