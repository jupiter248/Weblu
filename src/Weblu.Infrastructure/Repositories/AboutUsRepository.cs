using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Application.Strategies.AboutUsInfo;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Parameters;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class AboutUsRepository : GenericRepository<AboutUs, AboutUsParameters>, IAboutUsRepository
    {
        public AboutUsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<IReadOnlyList<AboutUs>> GetAllAsync(AboutUsParameters aboutUsParameters)
        {
            IQueryable<AboutUs> aboutUs = _context.AboutUs.AsNoTracking();

            if (aboutUsParameters.CreatedDateSort != CreatedDateSort.All)
            {
                aboutUs = new AboutUsQueryHandler(new CreatedDateSortStrategy())
                .ExecuteAboutUsQuery(aboutUs, aboutUsParameters);
            }

            return await aboutUs.ToListAsync();
        }
    }
}