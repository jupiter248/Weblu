using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Search;
using Weblu.Domain.Enums.Common.Search;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Events.Common;
using Weblu.Domain.Events.Portfolios;
using Weblu.Domain.Interfaces;

namespace Weblu.Application.EventHandlers.Portfolios
{
    public class PortfolioSearchIndexHandler : IDomainEventHandler<PortfolioAddedEvent>
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PortfolioSearchIndexHandler(ISearchRepository searchRepository, IPortfolioRepository portfolioRepository, IUnitOfWork unitOfWork)
        {
            _searchRepository = searchRepository;
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(PortfolioAddedEvent domainEvent)
        {
            Portfolio? portfolio = await _portfolioRepository.GetByPublicIdAsync(domainEvent.PortfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            SearchItem searchItem = new SearchItem()
            {
                EntityId = portfolio.Id,
                EntityType = SearchEntityType.Portfolio,
                Content = portfolio.Description,
                Title = portfolio.Title
            };
            await _searchRepository.IndexAsync(searchItem);
            await _unitOfWork.CommitAsync();
        }
    }
}