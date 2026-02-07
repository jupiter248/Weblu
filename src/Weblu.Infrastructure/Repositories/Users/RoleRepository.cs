using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Infrastructure.Repositories.Users
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetRolePermissionsAsync(IEnumerable<string> roleNames)
        {
            return await (
                from role in _context.Roles
                join claim in _context.RoleClaims
                    on role.Id equals claim.RoleId
                where roleNames.Contains(role.Name!)
                      && claim.ClaimType == CustomClaimTypes.Permission
                select claim.ClaimValue
            )
            .Distinct()
            .ToListAsync();
        }
    }
}