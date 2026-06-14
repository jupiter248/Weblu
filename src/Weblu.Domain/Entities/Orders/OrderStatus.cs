using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Orders;

public class OrderStatus : BaseEntity
{
    // Required properties
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<Order> Orders { get; set; } = new List<Order>();
}