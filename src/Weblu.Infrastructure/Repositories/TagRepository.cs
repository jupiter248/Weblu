using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Domain.Entities.Common;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }

        public void DeleteTag(Tag tag)
        {
            _context.Tags.Remove(tag);
        }

        public async Task<IReadOnlyList<Tag>> GetAllTagsAsync(Application.Parameters.TagParameters tagParameters)
        {
            IQueryable<Tag> tags = _context.Tags.AsQueryable();

            return await tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int tagId)
        {
            Tag? tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
            return tag;
        }

        public void UpdateTag(Tag tag)
        {
            _context.Tags.Update(tag);
        }
    }
}