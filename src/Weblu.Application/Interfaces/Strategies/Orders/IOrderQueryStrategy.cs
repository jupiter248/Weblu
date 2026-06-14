using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Interfaces.Strategies.Orders;

public interface IOrderQueryStrategy
{
    IQueryable<Order> Query(IQueryable<Order> orders, OrderParameters orderParameters);

}