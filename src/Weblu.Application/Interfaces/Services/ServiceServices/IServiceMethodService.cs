namespace Weblu.Application.Interfaces.Services.ServiceServices
{
    public interface IServiceMethodService
    {
        // Methods
        Task AddMethodAsync(int serviceId, int methodId);
        Task DeleteMethodAsync(int serviceId, int methodId);
    }
}