using Weblu.Application.Dtos.Portfolios.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Interfaces.Repositories.Portfolios;
using Weblu.Application.Interfaces.Services.Portfolios;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Services.Portfolios
{
    public class PortfolioImageService : IPortfolioImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IImageRepository _imageRepository;
        public PortfolioImageService(
            IUnitOfWork unitOfWork,
            IPortfolioRepository portfolioRepository,
            IImageRepository imageRepository
        )
        {
            _imageRepository = imageRepository;
            _portfolioRepository = portfolioRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(int portfolioId, int imageId, AddPortfolioImageDto addPortfolioImageDto)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdWithImagesAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            ImageMedia imageMedia = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            PortfolioImage newImage = new PortfolioImage()
            {
                ImageMedia = imageMedia,
                ImageMediaId = imageMedia.Id,
                Portfolio = portfolio,
                PortfolioId = portfolio.Id,
                IsThumbnail = addPortfolioImageDto.IsThumbnail
            };

            portfolio.AddImage(newImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int portfolioId, int imageId)
        {
            Portfolio portfolio = await _portfolioRepository.GetByIdWithImagesAsync(portfolioId) ?? throw new NotFoundException(PortfolioErrorCodes.PortfolioNotFound);
            ImageMedia imageMedia = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            portfolio.RemoveImage(imageMedia);
            await _unitOfWork.CommitAsync();
        }
    }
}