using Microsoft.EntityFrameworkCore;
using Weblu.Application.Strategies.About;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Common.Pagination;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Application.Parameters.About;
using Weblu.Application.Interfaces.Repositories.About;

namespace Weblu.Infrastructure.Repositories.About
{
    internal class AboutUsRepository : GenericRepository<AboutUs, AboutUsParameters>, IAboutUsRepository
    {
        public AboutUsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AboutUs?> GetAsync()
        {
            return await _context.AboutUs.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}