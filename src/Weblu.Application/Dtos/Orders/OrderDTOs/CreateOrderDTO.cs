namespace Weblu.Application.DTOs.Orders.OrderDTOs;

public class CreateOrderDTO
{
    public string Name { get; set; } = default!;

    public int ServiceId { get; set; } = default!;
    public int MethodId { get; set; } = default!;
}