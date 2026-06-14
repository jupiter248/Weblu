namespace Weblu.Application.DTOs.Orders.OrderStatusDTOs;

public class OrderStatusDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
}