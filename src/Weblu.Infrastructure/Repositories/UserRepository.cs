using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Dtos.CommentDtos;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentUserDto?> GetUserForCommentAsync(string userId)
        {
            CommentUserDto? commentUserDto = await _context.Users.Where(u => u.Id == userId).Select(c => new CommentUserDto()
            {
                UserId = c.Id,
                UserName = c.UserName,
                UserProfileUrl = c.Profiles.Where(P => P.IsMain).Select(u => u.Url).FirstOrDefault(),
                UserProfileAltText = c.Profiles.Where(P => P.IsMain).Select(u => u.AltText).FirstOrDefault()
            }).FirstOrDefaultAsync();

            return commentUserDto;
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            string adminRoleId = await _context.Roles.Where(r => r.Name == "Admin").Select(r => r.Id).FirstOrDefaultAsync() ?? default!;
            bool isAdmin = await _context.UserRoles.AnyAsync(u => u.UserId == userId && u.RoleId == adminRoleId);
            return isAdmin;
        }

        public async Task<bool> UserExistsAsync(string userId)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.Id == userId);

            return userExists;
        }
    }
}