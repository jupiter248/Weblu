using Weblu.Application.Common.Interfaces;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Domain.Interfaces.Repositories.Orders;

public interface IOrderRepository : IGenericRepository<Order, OrderParameters>
{

}