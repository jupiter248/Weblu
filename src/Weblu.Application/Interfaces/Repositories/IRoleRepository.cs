using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<List<string>> GetRolePermissionsAsync(IEnumerable<string> roleNames);

    }
}