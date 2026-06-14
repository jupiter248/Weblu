using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Orders.OrderDTOs;
using Weblu.Application.DTOs.Services.ServiceDTOs;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Interfaces.Services.Orders
{
    public interface IOrderService
    {
        Task<PagedResponse<OrderSummeryDTO>> GetAllPagedAsync(string userId, OrderParameters orderParameters);
        Task<OrderDetailDTO> GetByIdAsync(int orderId, string userId);
        Task<OrderDetailDTO> OrderAsync(CreateOrderDTO createOrderDTO, string userId);
        Task<OrderDetailDTO> UpdateAsync(string userId, int orderId, UpdateOrderDTO updateOrderDTO);
        Task DeleteAsync(int orderId, string userId);
        Task ChangeStatus(int orderId, int statusId);

    }
}