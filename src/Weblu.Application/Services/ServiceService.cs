using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Features;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Services;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Common;
using AutoMapper.QueryableExtensions;

namespace Weblu.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeatureRepository _featureRepository;
        private readonly IMethodRepository _methodRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceService> _logger;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ServiceService> logger
                , IFeatureRepository featureRepository,
                IMethodRepository methodRepository,
                IImageRepository imageRepository,
                IServiceRepository serviceRepository
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _featureRepository = featureRepository;
            _methodRepository = methodRepository;
            _imageRepository = imageRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task AddFeatureToServiceAsync(int serviceId, int featureId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (service.Features.Any(f => f.Id == featureId))
            {
                throw new ConflictException(ServiceErrorCodes.FeatureAlreadyAddedToService);
            }
            service.Features.Add(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddImageToService(int serviceId, int imageId, AddServiceImageDto addServiceImageDto)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            ImageMedia? image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            if (service.ServiceImages.Any(f => f.ImageId == imageId && f.ServiceId == serviceId))
            {
                throw new ConflictException(ServiceErrorCodes.ImageAlreadyAddedToService);
            }
            if (service.ServiceImages.Any(t => t.IsThumbnail && addServiceImageDto.IsThumbnail))
            {
                throw new ConflictException(ServiceErrorCodes.ServiceHasThumbnailImage);
            }

            ServiceImage serviceImage = new ServiceImage()
            {
                Image = image,
                ImageId = image.Id,
                Service = service,
                ServiceId = service.Id,
                IsThumbnail = addServiceImageDto.IsThumbnail
            };

            service.ServiceImages.Add(serviceImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddMethodToService(int serviceId, int methodId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            if (service.Methods.Any(m => m.Id == methodId))
            {
                throw new ConflictException(ServiceErrorCodes.MethodAlreadyAddedToService);
            }
            service.Methods.Add(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ServiceDetailDto> AddServiceAsync(AddServiceDto addServiceDto)
        {
            Service newService = _mapper.Map<Service>(addServiceDto);
            if (newService.IsActive)
            {
                newService.ActivatedAt = DateTimeOffset.Now;
            }
            _serviceRepository.Add(newService);
            await _unitOfWork.CommitAsync();
            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(newService);
            return serviceDto;
        }

        public async Task DeleteFeatureFromServiceAsync(int serviceId, int featureId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (!service.Features.Any(m => m.Id == featureId))
            {
                throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            }

            service.Features.Remove(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteImageToService(int serviceId, int imageId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            ImageMedia? image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ServiceImage? serviceImage = service.ServiceImages.FirstOrDefault(f => f.ImageId == imageId && f.ServiceId == serviceId);
            if (serviceImage == null)
            {
                throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            }
            service.ServiceImages.Remove(serviceImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodFromService(int serviceId, int methodId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (!service.Methods.Any(m => m.Id == methodId))
            {
                throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            }

            service.Methods.Remove(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteServiceAsync(int serviceId)
        {
            _logger.LogInformation($"Deleting service {serviceId}");
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            _serviceRepository.Delete(service);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ServiceSummaryDto>> GetAllServicesAsync(ServiceParameters serviceParameters)
        {
            IReadOnlyList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDto> serviceSummaryDtos = _mapper.Map<List<ServiceSummaryDto>>(services);
            return serviceSummaryDtos;
        }

        public async Task<ServiceDetailDto> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            List<ServiceImageDto> imageDtos = new List<ServiceImageDto>();
            foreach (var item in service.ServiceImages)
            {
                imageDtos.Add(_mapper.Map<ServiceImageDto>(item));
            }
            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(service);
            serviceDto.Images = imageDtos;
            return serviceDto;
        }

        public async Task<ServiceDetailDto> UpdateServiceAsync(int serviceId, UpdateServiceDto updateServiceDto)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            service = _mapper.Map(updateServiceDto, service);
            if (service.IsActive)
            {
                if (service.ActivatedAt == DateTimeOffset.MinValue)
                {
                    service.ActivatedAt = DateTimeOffset.Now;
                }
            }
            else if (!service.IsActive)
            {
                service.ActivatedAt = DateTimeOffset.MinValue;
            }
            _serviceRepository.Update(service);
            await _unitOfWork.CommitAsync();
            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(service);
            return serviceDto;
        }
    }
}