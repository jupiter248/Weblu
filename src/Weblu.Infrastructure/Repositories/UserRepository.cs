using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> UserExistsAsync(string userId)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.Id == userId);

            return userExists;
        }
    }
}