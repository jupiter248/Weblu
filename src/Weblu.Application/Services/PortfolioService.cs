using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Features;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PortfolioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task AddFeatureToPortfolioAsync(int portfolioId, int featureId)
        {
            Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Feature feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (portfolio.Features.Any(m => m.Id == featureId))
            {
                throw new ConflictException(PortfolioErrorCodes.FeatureAlreadyAddedToPortfolio);
            }

            portfolio.Features.Add(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddMethodToPortfolioAsync(int portfolioId, int methodId)
        {
            Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Method method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (portfolio.Methods.Any(m => m.Id == methodId))
            {
                throw new ConflictException(PortfolioErrorCodes.MethodAlreadyAddedToPortfolio);
            }

            portfolio.Methods.Add(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PortfolioDetailDto> AddPortfolioAsync(AddPortfolioDto addPortfolioDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(addPortfolioDto);

            await _unitOfWork.Portfolios.AddPortfolioAsync(portfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            return portfolioDetailDto;
        }

        public async Task DeleteFeatureFromPortfolioAsync(int portfolioId, int featureId)
        {
            Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Feature feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (!portfolio.Features.Any(f => f.Id == featureId))
            {
                throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            }

            portfolio.Features.Remove(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodFromPortfolioAsync(int portfolioId, int methodId)
        {
            Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Method method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (!portfolio.Methods.Any(f => f.Id == methodId))
            {
                throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            }

            portfolio.Methods.Remove(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePortfolioAsync(int portfolioId)
        {
            Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);

            _unitOfWork.Portfolios.DeletePortfolio(portfolio);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<PortfolioSummaryDto>> GetAllPortfolioAsync(PortfolioParameters portfolioParameters)
        {
            List<Portfolio> portfolios = await _unitOfWork.Portfolios.GetAllPortfolioAsync(portfolioParameters);
            List<PortfolioSummaryDto> portfolioSummaryDtos = _mapper.Map<List<PortfolioSummaryDto>>(portfolios);
            return portfolioSummaryDtos;
        }

        public async Task<PortfolioDetailDto> GetPortfolioByIdAsync(int portfolioId)
        {
            Portfolio portfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            return portfolioDetailDto;
        }

        public async Task<PortfolioDetailDto> UpdatePortfolioAsync(int portfolioId, UpdatePortfolioDto updatePortfolioDto)
        {
            Portfolio currentPortfolio = await _unitOfWork.Portfolios.GetPortfolioByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            currentPortfolio = _mapper.Map(updatePortfolioDto, currentPortfolio);

            if (currentPortfolio.IsActive)
            {
                if (currentPortfolio.ActivatedAt == DateTimeOffset.MinValue)
                {
                    currentPortfolio.ActivatedAt = DateTimeOffset.Now;
                }
            }
            else if (!currentPortfolio.IsActive)
            {
                currentPortfolio.ActivatedAt = DateTimeOffset.MinValue;
            }

            _unitOfWork.Portfolios.UpdatePortfolio(currentPortfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(currentPortfolio);
            return portfolioDetailDto;
        }
    }
}