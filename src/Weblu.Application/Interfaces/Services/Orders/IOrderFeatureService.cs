namespace Weblu.Application.Interfaces.Services.Orders
{
    public interface IOrderFeatureService
    {
        Task AddAsync(int orderId, int featureId);
        Task DeleteAsync(int orderId, int featureId);
    }
}