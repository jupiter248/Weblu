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
using Weblu.Domain.Entities.Methods;
using Weblu.Domain.Entities.Common;
using AutoMapper.QueryableExtensions;
using Weblu.Domain.Entities.Features;
using Weblu.Application.Interfaces.Services.ServiceServices;

namespace Weblu.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceService> _logger;
        public ServiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ServiceService> logger,
            IServiceRepository serviceRepository
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _serviceRepository = serviceRepository;
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