using Microsoft.EntityFrameworkCore;
using Weblu.Application.Strategies.Orders;
using Weblu.Domain.Common.Models;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Interfaces.Repositories.Orders;
using Weblu.Domain.Parameters.Orders;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories.Orders;

internal class OrderRepository(ApplicationDbContext context) : GenericRepository<Order, OrderParameters>(context), IOrderRepository
{

    public override async Task<PagedList<Order>> GetAllAsync(OrderParameters orderParameters)
    {

        var orders = _context.Orders.Where(a => !a.IsDeleted).Include(o => o.Status).Include(o => o.Service).Include(o => o.Method).AsQueryable();

        if (!string.IsNullOrEmpty(orderParameters.UserId))
        {
            orders = new OrderQueryHandler(new FilterByUserIdStrategy())
            .ExecuteOrderQuery(orders, orderParameters);
        }

        var pagedList = await PaginationExtensions<Order>.GetPagedList(orders, orderParameters.PageNumber, orderParameters.PageSize);

        return pagedList;
    }

    public override async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.Where(a => !a.IsDeleted).Include(o => o.Status).Include(o => o.Service).Include(o => o.Method).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order?> GetByIdWithFeaturesAsync(int id)
    {
        return await _context.Orders.Where(a => !a.IsDeleted).Include(o => o.Status).Include(o => o.Service).Include(o => o.Method).Include(o => o.Features).FirstOrDefaultAsync(o => o.Id == id);
    }
}