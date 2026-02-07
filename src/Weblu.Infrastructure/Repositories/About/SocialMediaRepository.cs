using Weblu.Domain.Entities.About;
using Weblu.Infrastructure.Data;
using Weblu.Infrastructure.Common.Repositories;
using Weblu.Application.Parameters.About;
using Weblu.Application.Interfaces.Repositories.About;

namespace Weblu.Infrastructure.Repositories.About
{
    internal class SocialMediaRepository : GenericRepository<SocialMedia, SocialMediaParameters>, ISocialMediaRepository
    {
        public SocialMediaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}