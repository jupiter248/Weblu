using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Errors.Features;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Services;
using Weblu.Domain.Interfaces;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceService> _logger;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ServiceService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddFeatureToServiceAsync(int serviceId, int featureId)
        {
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Feature? feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (service.Features.Any(f => f.Id == featureId))
            {
                throw new ConflictException(ServiceErrorCodes.FeatureAlreadyAddedToService);
            }
            service.Features.Add(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddMethodToService(int serviceId, int methodId)
        {
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Method? method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            if (service.Methods.Any(m => m.Id == methodId))
            {
                throw new ConflictException(ServiceErrorCodes.MethodAlreadyAddedToService);
            }
            service.Methods.Add(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ServiceDto> AddServiceAsync(AddServiceDto addServiceDto)
        {
            Service newService = _mapper.Map<Service>(addServiceDto);
            if (newService.IsActive)
            {
                newService.ActivatedAt = DateTimeOffset.Now;
            }
            await _unitOfWork.Services.AddServiceAsync(newService);
            await _unitOfWork.CommitAsync();
            ServiceDto serviceDto = _mapper.Map<ServiceDto>(newService);
            return serviceDto;
        }

        public async Task DeleteFeatureFromServiceAsync(int serviceId, int featureId)
        {
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Feature? feature = await _unitOfWork.Features.GetFeatureByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            if (!service.Features.Any(m => m.Id == featureId))
            {
                throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);
            }

            service.Features.Remove(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodFromService(int serviceId, int methodId)
        {
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Method? method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

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
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            _unitOfWork.Services.DeleteService(service);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ServiceDto>> GetAllServicesAsync(ServiceParameters serviceParameters)
        {
            List<Service> services = await _unitOfWork.Services.GetAllServicesAsync(serviceParameters);
            List<ServiceDto> serviceDtos = _mapper.Map<List<ServiceDto>>(services);
            return serviceDtos;
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            ServiceDto serviceDto = _mapper.Map<ServiceDto>(service);
            return serviceDto;
        }

        public async Task<ServiceDto> UpdateServiceAsync(int serviceId, UpdateServiceDto updateServiceDto)
        {
            Service? service = await _unitOfWork.Services.GetServiceByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            service = _mapper.Map(updateServiceDto, service);
            service.UpdatedAt = DateTimeOffset.Now;
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
            _unitOfWork.Services.UpdateService(service);
            await _unitOfWork.CommitAsync();
            ServiceDto serviceDto = _mapper.Map<ServiceDto>(service);
            return serviceDto;
        }
    }
}