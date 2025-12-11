using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Dtos.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Contributors;
using Weblu.Domain.Errors.Features;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.PortfolioCategory;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioCategoryRepository _portfolioCategoryRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IMethodRepository _methodRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IContributorRepository _contributorRepository;
        private readonly IMapper _mapper;
        public PortfolioService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPortfolioCategoryRepository portfolioCategoryRepository,
        IPortfolioRepository portfolioRepository,
        IFeatureRepository featureRepository,
        IImageRepository imageRepository,
        IMethodRepository methodRepository,
        IContributorRepository contributorRepository
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _portfolioCategoryRepository = portfolioCategoryRepository;
            _portfolioRepository = portfolioRepository;
            _featureRepository = featureRepository;
            _methodRepository = methodRepository;
            _imageRepository = imageRepository;
            _imageRepository = imageRepository;
            _contributorRepository = contributorRepository;
        }

        public async Task AddContributorToPortfolioAsync(int portfolioId, int contributorId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (portfolio.Contributors.Any(m => m.Id == contributorId))
            {
                throw new ConflictException(PortfolioErrorCodes.ContributorAlreadyAddedToPortfolio);
            }

            portfolio.Contributors.Add(contributor);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddFeatureToPortfolioAsync(int portfolioId, int featureId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (portfolio.Features.Any(m => m.Id == featureId))
            {
                throw new ConflictException(PortfolioErrorCodes.FeatureAlreadyAddedToPortfolio);
            }

            portfolio.Features.Add(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddImageToPortfolioAsync(int portfolioId, int imageId, AddPortfolioImageDto addPortfolioImageDto)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            ImageMedia imageMedia = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            if (portfolio.PortfolioImages.Any(p => p.ImageMediaId == imageMedia.Id))
            {
                throw new ConflictException(PortfolioErrorCodes.ImageAlreadyAddedToPortfolio);
            }
            if (portfolio.PortfolioImages.Any(p => p.IsThumbnail && addPortfolioImageDto.IsThumbnail))
            {
                throw new ConflictException(PortfolioErrorCodes.PortfolioHasThumbnailImage);
            }

            PortfolioImage newImage = new PortfolioImage()
            {
                ImageMedia = imageMedia,
                ImageMediaId = imageMedia.Id,
                Portfolio = portfolio,
                PortfolioId = portfolio.Id,
                IsThumbnail = addPortfolioImageDto.IsThumbnail
            };

            portfolio.PortfolioImages.Add(newImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddMethodToPortfolioAsync(int portfolioId, int methodId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Method method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

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

            PortfolioCategory portfolioCategory = await _portfolioCategoryRepository.GetByIdAsync(addPortfolioDto.PortfolioCategoryId) ?? throw new NotFoundException(PortfolioCategoryErrorCodes.PortfolioCategoryNotFound);
            portfolio.PortfolioCategory = portfolioCategory;

            _portfolioRepository.Add(portfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(portfolio);
            return portfolioDetailDto;
        }

        public async Task DeleteContributorFromPortfolioAsync(int portfolioId, int contributorId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            if (!portfolio.Contributors.Any(c => c.Id == contributorId))
            {
                throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            }
            portfolio.Contributors.Remove(contributor);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFeatureFromPortfolioAsync(int portfolioId, int featureId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Feature feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (!portfolio.Features.Any(f => f.Id == featureId))
            {
                throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            }

            portfolio.Features.Remove(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteImageFromPortfolioAsync(int portfolioId, int imageId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            ImageMedia imageMedia = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            PortfolioImage? portfolioImage = portfolio.PortfolioImages.FirstOrDefault(i => i.ImageMediaId == imageMedia.Id && i.PortfolioId == portfolio.Id);
            if (portfolioImage == null)
            {
                throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            }

            portfolio.PortfolioImages.Remove(portfolioImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodFromPortfolioAsync(int portfolioId, int methodId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            Method method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (!portfolio.Methods.Any(f => f.Id == methodId))
            {
                throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            }

            portfolio.Methods.Remove(method);
            await _unitOfWork.CommitAsync();
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

        public async Task<PortfolioDetailDto> GetPortfolioByIdAsync(int portfolioId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
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

            _portfolioRepository.Update(currentPortfolio);
            await _unitOfWork.CommitAsync();

            PortfolioDetailDto portfolioDetailDto = _mapper.Map<PortfolioDetailDto>(currentPortfolio);
            return portfolioDetailDto;
        }
    }
}