using Weblu.Application.DTOs.Services.ServiceDTOs.ServiceImageDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Interfaces.Repositories.Services;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Services;

namespace Weblu.Application.Services.ServiceServices
{
    public class ServiceImageService : IServiceImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;
        private readonly IImageRepository _imageRepository;
        public ServiceImageService(
            IUnitOfWork unitOfWork,
            IImageRepository imageRepository,
            IServiceRepository serviceRepository
        )
        {
            _unitOfWork = unitOfWork;
            _imageRepository = imageRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task AddAsync(int serviceId, int imageId, AddServiceImageDTO addServiceImageDTO)
        {
            Service? service = await _serviceRepository.GetByIdWithImagesAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            ImageMedia? image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            ServiceImage serviceImage = new ServiceImage()
            {
                Image = image,
                ImageId = image.Id,
                Service = service,
                ServiceId = service.Id,
                IsThumbnail = addServiceImageDTO.IsThumbnail
            };

            service.AddImage(serviceImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int serviceId, int imageId)
        {
            Service? service = await _serviceRepository.GetByIdWithImagesAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            ImageMedia? image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            service.RemoveImage(image);
            await _unitOfWork.CommitAsync();
        }
    }
}