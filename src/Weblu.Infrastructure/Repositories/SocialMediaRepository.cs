using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.About;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly ApplicationDbContext _context;
        public SocialMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSocialMediaAsync(SocialMedia socialMedia)
        {
            await _context.SocialMedias.AddAsync(socialMedia);
        }

        public void DeleteSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Remove(socialMedia);
        }

        public async Task<IReadOnlyList<SocialMedia>> GetAllSocialMediasAsync()
        {
            IQueryable<SocialMedia> socialMedias = _context.SocialMedias.AsQueryable();

            return await socialMedias.ToListAsync();
        }

        public async Task<SocialMedia?> GetSocialMediaByIdAsync(int socialMediaId)
        {
            SocialMedia? socialMedia = await _context.SocialMedias.FirstOrDefaultAsync(s => s.Id == socialMediaId);
            return socialMedia;
        }

        public void UpdateSocialMedia(SocialMedia socialMedia)
        {
            _context.SocialMedias.Update(socialMedia);
        }
    }
}