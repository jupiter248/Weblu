using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class PortfolioCategoryRepository :GenericRepository<PortfolioCategory, PortfolioCategoryParameters>, IPortfolioCategoryRepository
    {
        public PortfolioCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}