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
    public class AboutUsRepository : IAboutUsRepository
    {
        private readonly ApplicationDbContext _context;
        public AboutUsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAboutUsAsync(AboutUs aboutUs)
        {
            await _context.AboutUs.AddAsync(aboutUs);
        }

        public void DeleteAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Remove(aboutUs);
        }

        public async Task<AboutUs?> GetAboutUsInfoByIdAsync(int aboutUsId)
        {
            AboutUs? aboutUs = await _context.AboutUs.FirstOrDefaultAsync(a => a.Id == aboutUsId);
            return aboutUs;
        }

        public async Task<IReadOnlyList<AboutUs>> GetAllAboutUsInfosAsync(AboutUsParameters aboutUsParameters)
        {
            IQueryable<AboutUs> aboutUs = _context.AboutUs;

            if (aboutUsParameters.CreatedDateSort != CreatedDateSort.All)
            {
                aboutUs = new AboutUsQueryHandler(new CreatedDateSortStrategy())
                .ExecuteAboutUsQuery(aboutUs, aboutUsParameters);
            }

            return await aboutUs.ToListAsync();
        }

        public void UpdateAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Update(aboutUs);
        }
    }
}