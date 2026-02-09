using AutoMapper;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Services;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Application.Common.Responses;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Interfaces.Repositories.Services;
using Weblu.Application.DTOs.Services.ServiceDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Parameters.Services;
using Weblu.Application.DTOs.Services.ServiceDTOs.ServiceImageDTOs;
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
        public async Task<ServiceDetailDTO> CreateAsync(CreateServiceDTO createServiceDTO)
        {
            Service newService = _mapper.Map<Service>(createServiceDTO);
            _serviceRepository.Add(newService);
            await _unitOfWork.CommitAsync();
            ServiceDetailDTO serviceDTO = _mapper.Map<ServiceDetailDTO>(newService);
            return serviceDTO;
        }
        public async Task DeleteAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            if (service.IsPublished) throw new ConflictException(ServiceErrorCodes.IsPublish);
            service.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<PagedResponse<ServiceSummaryDTO>> GetAllPagedAsync(ServiceParameters serviceParameters)
        {
            PagedList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDTO> serviceSummaryDTOs = _mapper.Map<List<ServiceSummaryDTO>>(services);
            var pagedResponse = _mapper.Map<PagedResponse<ServiceSummaryDTO>>(services);
            pagedResponse.Items = serviceSummaryDTOs;
            return pagedResponse;
        }

        public async Task<List<ServiceSummaryDTO>> GetAllAsync(ServiceParameters serviceParameters)
        {
            IReadOnlyList<Service> services = await _serviceRepository.GetAllAsync(serviceParameters);
            List<ServiceSummaryDTO> serviceSummaryDTOs = _mapper.Map<List<ServiceSummaryDTO>>(services);
            return serviceSummaryDTOs;
        }
        public async Task<ServiceDetailDTO> GetByIdAsync(int serviceId)
        {
            Service? service = await _serviceRepository.GetByIdWithImagesAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            List<ServiceImageDTO> imageDTOs = service.ServiceImages.Select(x => _mapper.Map<ServiceImageDTO>(x)).ToList();
            ServiceDetailDTO serviceDTO = _mapper.Map<ServiceDetailDTO>(service);
            serviceDTO.Images = imageDTOs;
            return serviceDTO;
        }
        public async Task<ServiceDetailDTO> UpdateAsync(int serviceId, UpdateServiceDTO updateServiceDTO)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            service = _mapper.Map(updateServiceDTO, service);

            service.MarkUpdated();
            _serviceRepository.Update(service);
            await _unitOfWork.CommitAsync();

            ServiceDetailDTO serviceDTO = _mapper.Map<ServiceDetailDTO>(service);
            return serviceDTO;
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