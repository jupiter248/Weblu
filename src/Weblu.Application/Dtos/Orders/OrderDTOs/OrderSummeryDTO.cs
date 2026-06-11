namespace Weblu.Application.DTOs.Orders.OrderDTOs;

public class OrderSummeryDTO
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public bool IsUpdated { get; set; } = default!;

    public string OrderedAt { get; set; } = default!;

    public string StatusName { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public string Username { get; set; } = default!;
}