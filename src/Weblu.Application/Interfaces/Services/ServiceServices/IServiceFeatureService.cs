namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceFeatureService
    {
        Task AddAsync(int serviceId, int featureId);
        Task DeleteAsync(int serviceId, int featureId);
    }
}