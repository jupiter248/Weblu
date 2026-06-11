using Weblu.Application.DTOs.Orders.OrderStatusDTOs;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Interfaces.Services.Orders;

public interface IOrderStatusService
{
    Task<List<OrderStatusDTO>> GetAllAsync(OrderStatusParameters orderStatusParameters);
    Task<OrderStatusDTO> GetByIdAsync(int statusId);
    Task<OrderStatusDTO> CreateAsync(CreateOrderStatusDTO createOrderStatusDTO);
    Task<OrderStatusDTO> EditAsync(int statusId, UpdateOrderStatusDTO updateOrderStatusDTO);
    Task DeleteAsync(int statusId);
}