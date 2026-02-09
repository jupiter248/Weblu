using AutoMapper;
using Weblu.Application.DTOs.Portfolios.PortfolioCategoryDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
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
        public async Task<PortfolioCategoryDTO> CreateAsync(CreatePortfolioCategoryDTO createPortfolioCategoryDTO)
        {
            PortfolioCategory portfolioCategory = _mapper.Map<PortfolioCategory>(createPortfolioCategoryDTO);

            _portfolioCategoryRepository.Add(portfolioCategory);
            await _unitOfWork.CommitAsync();

            PortfolioCategoryDTO portfolioCategoryDTO = _mapper.Map<PortfolioCategoryDTO>(portfolioCategory);
            return portfolioCategoryDTO;
        }
        public async Task DeleteAsync(int categoryId)
        {
            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolioCategory.Delete();
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<PortfolioCategoryDTO>> GetAllAsync(PortfolioCategoryParameters portfolioCategoryParameters)
        {
            IReadOnlyList<PortfolioCategory> portfolioCategories = await _portfolioCategoryRepository.GetAllAsync(portfolioCategoryParameters);
            List<PortfolioCategoryDTO> portfolioCategoryDTOs = _mapper.Map<List<PortfolioCategoryDTO>>(portfolioCategories);
            return portfolioCategoryDTOs;
        }

        public async Task<PortfolioCategoryDTO> GetByIdAsync(int categoryId)
        {
            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            PortfolioCategoryDTO portfolioCategoryDTO = _mapper.Map<PortfolioCategoryDTO>(portfolioCategory);
            return portfolioCategoryDTO;
        }

        public async Task<PortfolioCategoryDTO> UpdateAsync(int categoryId, UpdatePortfolioCategoryDTO updatePortfolioCategoryDTO)
        {
            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolioCategory = _mapper.Map(updatePortfolioCategoryDTO, portfolioCategory);

            portfolioCategory.MarkUpdated();
            _portfolioCategoryRepository.Update(portfolioCategory);
            await _unitOfWork.CommitAsync();

            PortfolioCategoryDTO portfolioCategoryDTO = _mapper.Map<PortfolioCategoryDTO>(portfolioCategory);
            return portfolioCategoryDTO;
        }
    }
}