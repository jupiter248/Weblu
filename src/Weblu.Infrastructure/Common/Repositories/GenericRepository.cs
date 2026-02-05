using Microsoft.EntityFrameworkCore;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Common.Pagination;
using Weblu.Application.Common.Parameters;
using Weblu.Domain.Entities.Common;
using Weblu.Infrastructure.Common.Pagination;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Common.Repositories
{
    internal abstract class GenericRepository<TEntity, TEntityParameters>
        : IGenericRepository<TEntity, TEntityParameters>
        where TEntity : BaseEntity
        where TEntityParameters : BaseParameters
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

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            TEntity? entity = await _context.Set<TEntity>().Where(e => !e.IsDeleted).FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public virtual async Task<PagedList<TEntity>> GetAllAsync(TEntityParameters entityParameters)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(e => !e.IsDeleted).AsNoTracking();

            return await PaginationExtensions<TEntity>.GetPagedList(query, entityParameters.PageNumber, entityParameters.PageSize);
            ;
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            bool entityExists = await _context.Set<TEntity>().Where(a => !a.IsDeleted).AnyAsync(e => e.Id == id);
            return entityExists;
        }

        public async Task<TEntity?> GetByGuidIdAsync(Guid guidId)
        {
            TEntity? entity = await _context.Set<TEntity>().Where(a => !a.IsDeleted).FirstOrDefaultAsync(e => e.GuidId == guidId);
            return entity;
        }
    }
}