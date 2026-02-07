using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Portfolios;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Interfaces.Repositories.Portfolios
{
    public interface IPortfolioCategoryRepository : IGenericRepository<PortfolioCategory, PortfolioCategoryParameters>
    {

    }
}