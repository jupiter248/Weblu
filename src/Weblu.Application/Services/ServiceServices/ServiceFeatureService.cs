using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Services;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Services;

namespace Weblu.Application.Services.ServiceServices
{
    public class ServiceFeatureService : IServiceFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;
        private readonly IFeatureRepository _featureRepository;
        public ServiceFeatureService(
        IUnitOfWork unitOfWork,
        IFeatureRepository featureRepository,
        IServiceRepository serviceRepository
    )
        {
            _unitOfWork = unitOfWork;
            _featureRepository = featureRepository;
            _serviceRepository = serviceRepository;
        }
        public async Task AddFeatureAsync(int serviceId, int featureId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            await _serviceRepository.LoadFeaturesAsync(service);

            service.AddFeature(feature);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteFeatureAsync(int serviceId, int featureId)
        {
            Service? service = await _serviceRepository.GetByIdAsync(serviceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
            Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

            await _serviceRepository.LoadFeaturesAsync(service);

            service.DeleteFeature(feature);
            await _unitOfWork.CommitAsync();
        }
    }
}