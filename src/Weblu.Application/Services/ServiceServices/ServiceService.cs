using AutoMapper;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Services;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Common.Responses;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Interfaces.Repositories.Services;
using Weblu.Application.Dtos.Services.ServiceDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Parameters.Services;
using Weblu.Application.Dtos.Services.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Interfaces.Repositories;

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
        public async Task<ServiceDetailDto> CreateAsync(CreateServiceDto createServiceDto)
        {
            Service newService = _mapper.Map<Service>(createServiceDto);
            _serviceRepository.Add(newService);
            await _unitOfWork.CommitAsync();
            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(newService);
            return serviceDto;
        }
        public async Task DeleteAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            if (service.IsPublished) throw new ConflictException(ServiceErrorCodes.IsPublish);
            service.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<PagedResponse<ServiceSummaryDto>> GetAllPagedAsync(ServiceParameters serviceParameters)
        {
            PagedList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDto> serviceSummaryDtos = _mapper.Map<List<ServiceSummaryDto>>(services);
            var pagedResponse = _mapper.Map<PagedResponse<ServiceSummaryDto>>(services);
            pagedResponse.Items = serviceSummaryDtos;
            return pagedResponse;
        }

        public async Task<List<ServiceSummaryDto>> GetAllAsync(ServiceParameters serviceParameters)
        {
            IReadOnlyList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDto> serviceSummaryDtos = _mapper.Map<List<ServiceSummaryDto>>(services);
            return serviceSummaryDtos;
        }
        public async Task<ServiceDetailDto> GetByIdAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdWithImagesAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            List<ServiceImageDto> imageDtos = service.ServiceImages.Select(x => _mapper.Map<ServiceImageDto>(x)).ToList();
            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(service);
            serviceDto.Images = imageDtos;
            return serviceDto;
        }
        public async Task<ServiceDetailDto> UpdateAsync(int serviceId, UpdateServiceDto updateServiceDto)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            service = _mapper.Map(updateServiceDto, service);

            service.MarkUpdated();
            _serviceRepository.Update(service);
            await _unitOfWork.CommitAsync();

            ServiceDetailDto serviceDto = _mapper.Map<ServiceDetailDto>(service);
            return serviceDto;
        }

        public async Task Publish(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);

            service.Publish();

            await _unitOfWork.CommitAsync();
        }

        public async Task Unpublish(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);

            service.Unpublish();

            await _unitOfWork.CommitAsync();
        }
    }
}