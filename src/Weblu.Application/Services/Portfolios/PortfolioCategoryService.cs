using AutoMapper;
using Weblu.Application.Dtos.Portfolios.PortfolioCategory;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services.Portfolios
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
            portfolioCategory.Delete();
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