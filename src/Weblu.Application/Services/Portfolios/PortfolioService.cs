using AutoMapper;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Responses;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Dtos.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.PortfolioCategory;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPortfolioCategoryRepository _portfolioCategoryRepository;
        private readonly IMapper _mapper;
        public PortfolioService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPortfolioCategoryRepository portfolioCategoryRepository,
        IPortfolioRepository portfolioRepository
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _portfolioCategoryRepository = portfolioCategoryRepository;
            _portfolioRepository = portfolioRepository;
        }

        public async Task<PortfolioDetailDto> AddPortfolioAsync(AddPortfolioDto addPortfolioDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(addPortfolioDto);

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(addPortfolioDto.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolio.PortfolioCategory = portfolioCategory;

            _portfolioRepository.Add(portfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            return portfolioDetailDto;
        }


        public async Task DeletePortfolioAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            _portfolioRepository.Delete(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<PortfolioSummaryDto>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters)
        {
            IReadOnlyList<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolioParameters);
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios);
            return portfolioSummaryDtos;
        }
        public async Task<PagedResponse<PortfolioSummaryDto>> GetAllPagedPortfolioAsync(PortfolioParameters portfolioParameters)
        {
            PagedList<Portfolio> portfolios = await _portfolioRepository.GetAllAsync(portfolioParameters);
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios);
            var pagedResponse = _mapper.Map<PagedResponse<PortfolioSummaryDto>>(portfolios);
            pagedResponse.Items = portfolioSummaryDtos;
            return pagedResponse;
        }

        public async Task<PortfolioDetailDto> GetPortfolioByIdAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdWithImagesAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            List<PortfolioImageDto> imageDtos = new List<PortfolioImageDto>();
            foreach (PortfolioImage item in portfolio.PortfolioImages)
            {
                imageDtos.Add(_mapper.Map<PortfolioImageDto>(item));
            }
            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            portfolioDetailDto.Images = imageDtos;
            return portfolioDetailDto;
        }

        public async Task<PortfolioDetailDto> UpdatePortfolioAsync(int portfolioId, UpdatePortfolioDto updatePortfolioDto)
        {
            Portfolio currentPortfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            currentPortfolio = _mapper.Map(updatePortfolioDto, currentPortfolio);

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(updatePortfolioDto.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            currentPortfolio.PortfolioCategory = portfolioCategory;

            currentPortfolio.UpdateActivateStatus(updatePortfolioDto.IsActive);

            _portfolioRepository.Update(currentPortfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(currentPortfolio);
            return portfolioDetailDto;
        }
    }
}