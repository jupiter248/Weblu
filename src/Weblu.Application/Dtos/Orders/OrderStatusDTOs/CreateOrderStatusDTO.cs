namespace Weblu.Application.DTOs.Orders.OrderStatusDTOs;

public class CreateOrderStatusDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}