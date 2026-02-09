using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services.Portfolios
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioCategoryRepository _portfolioCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public PortfolioService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPortfolioCategoryRepository portfolioCategoryRepository,
        IPortfolioRepository portfolioRepository,
        IDomainEventDispatcher domainEventDispatcher
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _portfolioCategoryRepository = portfolioCategoryRepository;
            _portfolioRepository = portfolioRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<PortfolioDetailDto> CreateAsync(CreatePortfolioDto createPortfolioDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(createPortfolioDto);

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(createPortfolioDto.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolio.PortfolioCategory = portfolioCategory;

            _portfolioRepository.Add(portfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            return portfolioDetailDto;
        }
        public async Task DeleteAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            if (portfolio.IsPublished) throw new ConflictException(PortfolioErrorCodes.IsPublish);
            portfolio.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<PortfolioSummaryDto>> GetAllAsync(PortfolioParameters portfolioParameters)
        {
            IReadOnlyList<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolioParameters);
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios);
            return portfolioSummaryDtos;
        }
        public async Task<PagedResponse<PortfolioSummaryDto>> GetAllPagedAsync(PortfolioParameters portfolioParameters)
        {
            PagedList<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolioParameters);
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios);
            var pagedResponse = _mapper.Map<PagedResponse<PortfolioSummaryDto>>(portfolios);
            pagedResponse.Items = portfolioSummaryDtos;
            return pagedResponse;
        }

        public async Task<PortfolioDetailDto> GetByIdAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdWithImagesAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            List<PortfolioImageDto> imageDtos = portfolio.PortfolioImages.Select(x => _mapper.Map<PortfolioImageDto>(x)).ToList();
            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            portfolioDetailDto.Images = imageDtos;
            return portfolioDetailDto;
        }

        public async Task Publish(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            portfolio.Publish();
            await _domainEventDispatcher.DispatchAsync(portfolio.Events);
            portfolio.ClearEvents();

            await _unitOfWork.CommitAsync();
        }

        public async Task Unpublish(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            portfolio.Unpublish();
            await _domainEventDispatcher.DispatchAsync(portfolio.Events);
            portfolio.ClearEvents();

            await _unitOfWork.CommitAsync();
        }

        public async Task<PortfolioDetailDto> UpdateAsync(int portfolioId, UpdatePortfolioDto updatePortfolioDto)
        {
            Portfolio currentPortfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            currentPortfolio = _mapper.Map(updatePortfolioDto, currentPortfolio);

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(updatePortfolioDto.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            currentPortfolio.PortfolioCategory = portfolioCategory;

            currentPortfolio.Update();
            _portfolioRepository.Update(currentPortfolio);
            await _unitOfWork.CommitAsync();

            await _domainEventDispatcher.DispatchAsync(currentPortfolio.Events);
            currentPortfolio.ClearEvents();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(currentPortfolio);
            return portfolioDetailDto;
        }
    }
}