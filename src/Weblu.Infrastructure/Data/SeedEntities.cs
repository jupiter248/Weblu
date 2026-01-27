using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Weblu.Infrastructure.Identity.Authorization;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Data
{
    public class SeedEntities
    {
        public static async Task SeedUserAndAdminAsync(ApplicationDbContext _context)
        {
            string[] roles = { Roles.HeadAdmin, Roles.Admin, Roles.Editor, Roles.User };
            //Add a user and an admin
            foreach (string item in roles)
            {
                if (!_context.Users.Any(u => u.NormalizedUserName == item.ToUpper()))
                {
                    AppUser user = new AppUser()
                    {
                        UserName = item.ToLower(),
                        Email = "mmazimifar7@gmail.com",
                        FirstName = item,
                        LastName = item,
                        PhoneNumber = "989031883414"
                    };
                    var hasher = new PasswordHasher<AppUser>();

                    user.PasswordHash = hasher.HashPassword(user, $"@{item}248");
                    user.NormalizedUserName = user.UserName.ToUpper();
                    user.NormalizedEmail = user.Email.ToUpper();

                    IdentityRole role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == user.UserName.ToUpper()) ?? default!;
                    IdentityUserRole<string> userRole = new IdentityUserRole<string>();
                    userRole.RoleId = role.Id;
                    userRole.UserId = user.Id;

                    _context.UserRoles.Add(userRole);
                    _context.Users.Add(user);
                }
            }
            await _context.SaveChangesAsync();
        }
        public static async Task SeedRolesWithClaimsAsync(ApplicationDbContext _context)
        {
            const string PermissionClaimType = "Permission";
            var rolesWithClaims = new Dictionary<string, string[]>
            {
                [Roles.HeadAdmin] =
                [
                    Permissions.ManageAdmins,
                    Permissions.ManageUsers,
                    Permissions.ManageComments,
                    Permissions.ManageService,
                    Permissions.ManagePortfolio,
                    Permissions.ManageArticle,
                    Permissions.ViewService,
                    Permissions.ViewPortfolio,
                    Permissions.ViewArticle,

                ],
                [Roles.Admin] =
                [
                    Permissions.ManageUsers,
                    Permissions.ManageComments,
                    Permissions.ManageService,
                    Permissions.ManagePortfolio,
                    Permissions.ManageArticle,
                    Permissions.ViewService,
                    Permissions.ViewPortfolio,
                    Permissions.ViewArticle,
                ],
                [Roles.Editor] =
                [
                    Permissions.ManageService,
                    Permissions.ManagePortfolio,
                    Permissions.ManageArticle,
                    Permissions.ViewService,
                    Permissions.ViewPortfolio,
                    Permissions.ViewArticle,
                ],
                [Roles.User] =
                [
                    Permissions.ViewService,
                    Permissions.ViewPortfolio,
                    Permissions.ViewArticle,
                ]
            };

            foreach (var (roleName, permissions) in rolesWithClaims)
            {
                // Get or create role
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Name == roleName);

                if (role is null)
                {
                    role = new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpperInvariant()
                    };

                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();
                }

                // Get existing claims for this role
                var existingClaims = await _context.RoleClaims
                    .Where(c => c.RoleId == role.Id && c.ClaimType == PermissionClaimType)
                    .Select(c => c.ClaimValue)
                    .ToListAsync();

                // Add missing claims
                var missingClaims = permissions.Except(existingClaims);

                foreach (var permission in missingClaims)
                {
                    _context.RoleClaims.Add(new IdentityRoleClaim<string>
                    {
                        RoleId = role.Id,
                        ClaimType = PermissionClaimType,
                        ClaimValue = permission
                    });
                }
            }
        }
    }
}