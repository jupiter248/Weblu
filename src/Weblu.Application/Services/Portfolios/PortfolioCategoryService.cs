using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.PortfolioCategory;

namespace Weblu.Application.Services
{
    public class PortfolioCategoryService : IPortfolioCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioCategoryRepository _portfolioCategoryRepository;
        private readonly IMapper _mapper;

        public PortfolioCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IPortfolioCategoryRepository portfolioCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _portfolioCategoryRepository = portfolioCategoryRepository;
        }
        public async Task<PortfolioCategoryDto> AddPortfolioCategoryAsync(AddPortfolioCategoryDto addPortfolioCategoryDto)
        {
            PortfolioCategory portfolioCategory = _mapper.Map<PortfolioCategory>(addPortfolioCategoryDto);

            _portfolioCategoryRepository.Add(portfolioCategory);
            await _unitOfWork.CommitAsync();

            PortfolioCategoryDto portfolioCategoryDto = _mapper.Map<PortfolioCategoryDto>(portfolioCategory);
            return portfolioCategoryDto;
        }

        public async Task DeletePortfolioCategoryAsync(int categoryId)
        {
            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            _portfolioCategoryRepository.Delete(portfolioCategory);
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<PortfolioCategoryDto>> GetAllPortfolioCategoriesAsync(PortfolioCategoryParameters portfolioCategoryParameters)
        {
            IReadOnlyList<PortfolioCategory> portfolioCategories = await _portfolioCategoryRepository.GetAllAsync(portfolioCategoryParameters);
            List<PortfolioCategoryDto> portfolioCategoryDtos = _mapper.Map<List<PortfolioCategoryDto>>(portfolioCategories);
            return portfolioCategoryDtos;
        }

        public async Task<PortfolioCategoryDto> GetPortfolioCategoryByIdAsync(int categoryId)
        {
            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            PortfolioCategoryDto portfolioCategoryDto = _mapper.Map<PortfolioCategoryDto>(portfolioCategory);
            return portfolioCategoryDto;
        }

        public async Task<PortfolioCategoryDto> UpdatePortfolioCategoryAsync(int categoryId, UpdatePortfolioCategoryDto updatePortfolioCategoryDto)
        {
            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolioCategory = _mapper.Map(updatePortfolioCategoryDto, portfolioCategory);

            _portfolioCategoryRepository.Update(portfolioCategory);
            await _unitOfWork.CommitAsync();

            PortfolioCategoryDto portfolioCategoryDto = _mapper.Map<PortfolioCategoryDto>(portfolioCategory);
            return portfolioCategoryDto;
        }
    }
}