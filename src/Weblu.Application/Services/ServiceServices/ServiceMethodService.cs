using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Services;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Services;

namespace Weblu.Application.Services.ServiceServices
{
    public class ServiceMethodService : IServiceMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMethodRepository _methodRepository;
        public ServiceMethodService(
            IUnitOfWork unitOfWork,
            IMethodRepository methodRepository,
            IServiceRepository serviceRepository
        )
        {
            _unitOfWork = unitOfWork;
            _methodRepository = methodRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task AddMethodAsync(int serviceId, int methodId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            await _serviceRepository.LoadMethodsAsync(service);

            service.AddMethod(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodAsync(int serviceId, int methodId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            await _serviceRepository.LoadMethodsAsync(service);

            service.DeleteMethod(method);
            await _unitOfWork.CommitAsync();
        }
    }
}