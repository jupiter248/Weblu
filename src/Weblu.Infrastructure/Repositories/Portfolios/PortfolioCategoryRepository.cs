using Weblu.Domain.Entities.Portfolios;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Application.Interfaces.Repositories.Portfolios;

namespace Weblu.Infrastructure.Repositories.Portfolios
{
    internal class PortfolioCategoryRepository :GenericRepository<PortfolioCategory, PortfolioCategoryParameters>, IPortfolioCategoryRepository
    {
        public PortfolioCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}