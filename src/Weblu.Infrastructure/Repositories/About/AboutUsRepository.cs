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
        public override async Task<PagedList<AboutUs>> GetAllAsync(AboutUsParameters aboutUsParameters)
        {
            IQueryable<AboutUs> aboutUs = _context.AboutUs.Where(a => !a.IsDeleted).AsNoTracking();

            if (aboutUsParameters.CreatedDateSort != CreatedDateSort.All)
            {
                aboutUs = new AboutUsQueryHandler(new CreatedDateSortStrategy())
                .ExecuteAboutUsQuery(aboutUs, aboutUsParameters);
            }

            return await PaginationExtensions<AboutUs>.GetPagedList(aboutUs, aboutUsParameters.PageNumber, aboutUsParameters.PageSize);

        }
    }
}