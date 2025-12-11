using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Common.Interfaces;
using Weblu.Domain.Entities.Common;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal abstract class GenericRepository<TEntity, TEntityParameters>
        : IGenericRepository<TEntity, TEntityParameters>
        where TEntity : BaseEntity
        where TEntityParameters : class
    {
        protected readonly ApplicationDbContext _context;
        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            TEntity? entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(TEntityParameters entityParameters)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return await query.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            bool entityExists = await _context.Set<TEntity>().AnyAsync(e => e.Id == id);
            return entityExists;
        }
    }
}