using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    internal class SocialMediaRepository : GenericRepository<SocialMedia, SocialMediaParameters>, ISocialMediaRepository
    {
        public SocialMediaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}