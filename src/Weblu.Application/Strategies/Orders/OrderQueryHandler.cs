using Weblu.Application.Interfaces.Strategies.Orders;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Strategies.Orders;

public class OrderQueryHandler
{
    private IOrderQueryStrategy _orderQueryStrategy;
    public OrderQueryHandler(IOrderQueryStrategy orderQueryStrategy)
    {
        _orderQueryStrategy = orderQueryStrategy;
    }
    public IQueryable<Order> ExecuteOrderQuery(IQueryable<Order> orders, OrderParameters orderParameters)
    {
        return _orderQueryStrategy.Query(orders, orderParameters);
    }
}