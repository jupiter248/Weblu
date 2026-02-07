using Weblu.Domain.Entities.Services;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Services;

namespace Weblu.Application.Interfaces.Repositories.Services
{
    public interface IServiceRepository : IGenericRepository<Service, ServiceParameters>
    {
        Task LoadMethodsAsync(Service service);
        Task LoadFeaturesAsync(Service service);
        Task<Service?> GetByIdWithImagesAsync(int serviceId);
    }
}