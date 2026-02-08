namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceMethodService
    {
        // Methods
        Task AddAsync(int serviceId, int methodId);
        Task DeleteAsync(int serviceId, int methodId);
    }
}