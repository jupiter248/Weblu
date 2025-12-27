using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblu.Application.Common.Models;
using Weblu.Domain.Enums.Users;
using Weblu.Infrastructure.Identity;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Data
{
    public class SeedEntities
    {
        public static async Task SeedUserAndAdmin(ApplicationDbContext _context)
        {
            //Add a user and an admin
            if (!_context.Users.Any(u => u.NormalizedUserName == "ADMIN"))
            {
                AppUser admin = new AppUser()
                {
                    UserName = "admin",
                    Email = "mmazimifar7@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "989031883414"
                };

                var hasher = new PasswordHasher<AppUser>();

                admin.PasswordHash = hasher.HashPassword(admin, "@Admin248");

                admin.NormalizedUserName = admin.UserName.ToUpper();
                admin.NormalizedEmail = admin.Email.ToUpper();


                IdentityRole role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == admin.UserName.ToUpper()) ?? default!;

                IdentityUserRole<string> userRole = new IdentityUserRole<string>();
                userRole.RoleId = role.Id;
                userRole.UserId = admin.Id;

                _context.UserRoles.Add(userRole);
                _context.Users.Add(admin);
            }
            if (!_context.Users.Any(u => u.NormalizedUserName == "ADMIN"))
            {
                AppUser user = new AppUser()
                {
                    UserName = "user",
                    Email = "mmazimifar7@gmail.com",
                    FirstName = "User",
                    LastName = "User",
                    PhoneNumber = "989031883414"
                };

                var hasher = new PasswordHasher<AppUser>();

                user.PasswordHash = hasher.HashPassword(user, "@User248");
                user.NormalizedUserName = user.UserName.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();

                IdentityRole role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == user.UserName.ToUpper()) ?? default!;

                IdentityUserRole<string> userRole = new IdentityUserRole<string>();
                userRole.RoleId = role.Id;
                userRole.UserId = user.Id;

                _context.UserRoles.Add(userRole);
                _context.Users.Add(user);

            }
            await _context.SaveChangesAsync();
        }
        public static async Task SeedRolesWithClaimsAsync(ApplicationDbContext _context)
        {

            var rolesWithClaims = new Dictionary<string, List<string>>
            {
                ["Head-Admin"] = new List<string>
                {
                    Permissions.ManageUsers,
                    Permissions.ViewDashboard,
                    Permissions.AddAdmin
                },
                ["Admin"] = new List<string>
                {
                    Permissions.ManageUsers,
                    Permissions.ViewDashboard
                },
                ["User"] = new List<string>
                {
                    Permissions.ViewDashboard
                }
            };

            foreach (var roleEntry in rolesWithClaims)
            {
                string roleName = roleEntry.Key;
                var claims = roleEntry.Value;

                // Create role if it doesn't exist
                if (!_context.Roles.Any(x => x.Name == roleName))
                {
                    IdentityRole role = new IdentityRole() ?? default!;
                    role.Name = roleName;
                    role.NormalizedName = role.Name.ToUpper();

                    _context.Roles.Add(role);

                    // Add missing claims to role

                    var existingClaims = await _context.RoleClaims.ToListAsync();
                    foreach (var claimValue in claims)
                    {
                        if (!existingClaims.Any(c => c.ClaimType == "permission" && c.ClaimValue == claimValue))
                        {
                            var claim = new IdentityRoleClaim<string>();
                            claim.ClaimValue = claimValue;
                            claim.ClaimType = claim.ClaimType;
                            claim.RoleId = role.Id;
                            _context.RoleClaims.Add(claim);
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}