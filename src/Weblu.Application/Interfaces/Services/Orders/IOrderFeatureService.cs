namespace Weblu.Application.Interfaces.Services.Orders
{
    public interface IOrderFeatureService
    {
        Task AddAsync(string userId, int orderId, int featureId);
        Task DeleteAsync(string userId, int orderId, int featureId);
    }
}