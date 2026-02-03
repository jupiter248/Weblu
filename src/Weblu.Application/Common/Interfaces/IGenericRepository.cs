using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Pagination;
using Weblu.Domain.Entities.Common;

namespace Weblu.Application.Common.Interfaces
{
    public interface IGenericRepository<TEntity, TEntityParameters>
     where TEntity : BaseEntity
     where TEntityParameters : class
    {
        Task<PagedList<TEntity>> GetAllAsync(TEntityParameters entityParameters);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetByPublicIdAsync(Guid publicId);
        Task<bool> ExistsAsync(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}