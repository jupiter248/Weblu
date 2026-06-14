using Weblu.Application.Interfaces.Strategies.Orders;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Strategies.Orders;

public class FilterByUserIdStrategy : IOrderQueryStrategy
{
    public IQueryable<Order> Query(IQueryable<Order> orders, OrderParameters orderParameters)
    {
        return orders.Where(p => p.OwnerId == orderParameters.UserId);

    }
}