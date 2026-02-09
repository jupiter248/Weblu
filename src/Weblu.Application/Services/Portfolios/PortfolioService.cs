using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs.PortfolioImageDTOs;
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

        public async Task<PortfolioDetailDTO> CreateAsync(CreatePortfolioDTO createPortfolioDTO)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(createPortfolioDTO);

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(createPortfolioDTO.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolio.PortfolioCategory = portfolioCategory;

            _portfolioRepository.Add(portfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDTO portfolioDetailDTO = _mapper.Map<PortfolioDetailDTO>(portfolio);
            return portfolioDetailDTO;
        }
        public async Task DeleteAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            if (portfolio.IsPublished) throw new ConflictException(PortfolioErrorCodes.IsPublish);
            portfolio.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<PortfolioSummaryDTO>> GetAllAsync(PortfolioParameters portfolioParameters)
        {
            IReadOnlyList<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolioParameters);
            List<PortfolioSummaryDTO> portfolioSummaryDTOs = _mapper.Map<List<PortfolioSummaryDTO>>(portfolios);
            return portfolioSummaryDTOs;
        }
        public async Task<PagedResponse<PortfolioSummaryDTO>> GetAllPagedAsync(PortfolioParameters portfolioParameters)
        {
            PagedList<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolioParameters);
            List<PortfolioSummaryDTO> portfolioSummaryDTOs = _mapper.Map<List<PortfolioSummaryDTO>>(portfolios);
            var pagedResponse = _mapper.Map<PagedResponse<PortfolioSummaryDTO>>(portfolios);
            pagedResponse.Items = portfolioSummaryDTOs;
            return pagedResponse;
        }

        public async Task<PortfolioDetailDTO> GetByIdAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdWithImagesAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            List<PortfolioImageDTO> imageDTOs = portfolio.PortfolioImages.Select(x => _mapper.Map<PortfolioImageDTO>(x)).ToList();
            PortfolioDetailDTO portfolioDetailDTO = _mapper.Map<PortfolioDetailDTO>(portfolio);
            portfolioDetailDTO.Images = imageDTOs;
            return portfolioDetailDTO;
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

        public async Task<PortfolioDetailDTO> UpdateAsync(int portfolioId, UpdatePortfolioDTO updatePortfolioDTO)
        {
            Portfolio currentPortfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            currentPortfolio = _mapper.Map(updatePortfolioDTO, currentPortfolio);

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(updatePortfolioDTO.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            currentPortfolio.PortfolioCategory = portfolioCategory;

            currentPortfolio.Update();
            _portfolioRepository.Update(currentPortfolio);
            await _unitOfWork.CommitAsync();

            await _domainEventDispatcher.DispatchAsync(currentPortfolio.Events);
            currentPortfolio.ClearEvents();

            PortfolioDetailDTO portfolioDetailDTO = _mapper.Map<PortfolioDetailDTO>(currentPortfolio);
            return portfolioDetailDTO;
        }
    }
}