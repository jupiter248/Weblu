using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Errors.Services;
using Weblu.Domain.Interfaces;
using Weblu.Domain.Parameters;

namespace Weblu.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task DeleteServiceAsync(int serviceId)
        {
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
            else if(!service.IsActive)
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