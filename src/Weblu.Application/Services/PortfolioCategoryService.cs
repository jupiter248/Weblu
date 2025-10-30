using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.PortfolioCategory;

namespace Weblu.Application.Services
{
    public class PortfolioCategoryService : IPortfolioCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortfolioCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PortfolioCategoryDto> AddPortfolioCategoryAsync(AddPortfolioCategoryDto addPortfolioCategoryDto)
        {
            PortfolioCategory portfolioCategory = _mapper.Map<PortfolioCategory>(addPortfolioCategoryDto);

            await _unitOfWork.PortfolioCategories.AddPortfolioCategoryAsync(portfolioCategory);
            await _unitOfWork.CommitAsync();

            PortfolioCategoryDto portfolioCategoryDto = _mapper.Map<PortfolioCategoryDto>(portfolioCategory);
            return portfolioCategoryDto;
        }

        public async Task DeletePortfolioCategoryAsync(int categoryId)
        {
            PortfolioCategory portfolioCategory = await _unitOfWork.PortfolioCategories.GetPortfolioCategoryByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            _unitOfWork.PortfolioCategories.DeletePortfolioCategory(portfolioCategory);
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<PortfolioCategoryDto>> GetAllPortfolioCategoriesAsync()
        {
            List<PortfolioCategory> portfolioCategories = await _unitOfWork.PortfolioCategories.GetAllPortfolioCategoriesAsync();
            List<PortfolioCategoryDto> portfolioCategoryDtos = _mapper.Map<List<PortfolioCategoryDto>>(portfolioCategories);
            return portfolioCategoryDtos;
        }

        public async Task<PortfolioCategoryDto> GetPortfolioCategoryByIdAsync(int categoryId)
        {
            PortfolioCategory portfolioCategory = await _unitOfWork.PortfolioCategories.GetPortfolioCategoryByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            PortfolioCategoryDto portfolioCategoryDto = _mapper.Map<PortfolioCategoryDto>(portfolioCategory);
            return portfolioCategoryDto;
        }

        public async Task<PortfolioCategoryDto> UpdatePortfolioCategoryAsync(int categoryId, UpdatePortfolioCategoryDto updatePortfolioCategoryDto)
        {
            PortfolioCategory portfolioCategory = await _unitOfWork.PortfolioCategories.GetPortfolioCategoryByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolioCategory = _mapper.Map(updatePortfolioCategoryDto, portfolioCategory);

            _unitOfWork.PortfolioCategories.UpdatePortfolioCategory(portfolioCategory);
            await _unitOfWork.CommitAsync();

            PortfolioCategoryDto portfolioCategoryDto = _mapper.Map<PortfolioCategoryDto>(portfolioCategory);
            return portfolioCategoryDto;
        }
    }
}