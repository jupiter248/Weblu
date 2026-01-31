using AutoMapper;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Services;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Common.Responses;
using Weblu.Application.Common.Pagination;

namespace Weblu.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceRepository serviceRepository
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            _serviceRepository.Delete(service);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResponse<ServiceSummaryDto>> GetAllPagedServiceAsync(ServiceParameters serviceParameters)
        {
            PagedList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDto> serviceSummaryDtos = _mapper.Map<List<ServiceSummaryDto>>(services);
            var pagedResponse = _mapper.Map<PagedResponse<ServiceSummaryDto>>(services);
            pagedResponse.Items = serviceSummaryDtos;
            return pagedResponse;
        }

        public async Task<List<ServiceSummaryDto>> GetAllServicesAsync(ServiceParameters serviceParameters)
        {
            IReadOnlyList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDto> serviceSummaryDtos = _mapper.Map<List<ServiceSummaryDto>>(services);
            return serviceSummaryDtos;
        }
        public async Task<ServiceDetailDto> GetServiceByIdAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdWithImagesAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
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
            service.UpdateActivateStatus(updateServiceDto.IsActive);
            _serviceRepository.Update(service);
            await _unitOfWork.CommitAsync();
            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(service);
            return serviceDto;
        }
    }
}