using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}