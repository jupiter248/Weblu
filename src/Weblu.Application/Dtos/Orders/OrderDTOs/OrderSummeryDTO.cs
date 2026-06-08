namespace Weblu.Application.DTOs.Orders.OrderDTOs;

public class OrderSummeryDTO
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;

    public string OrderedAt { get; set; } = default!;

    public int ServiceId { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public int MethodId { get; set; } = default!;
    public string MethodName { get; set; } = default!;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = default!;
    public string OwnerId { get; set; } = default!;
    public string Username { get; set; } = default!;
}