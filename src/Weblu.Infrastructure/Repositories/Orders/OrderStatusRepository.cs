using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Interfaces.Repositories.Orders;
using Weblu.Domain.Parameters.Orders;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories.Orders;
internal class OrderStatusRepository(ApplicationDbContext context) : GenericRepository<OrderStatus, OrderStatusParameters>(context), IOrderStatusRepository
{
    
}