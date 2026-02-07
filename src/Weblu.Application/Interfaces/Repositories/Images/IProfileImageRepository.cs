using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Images;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Interfaces.Repositories.Images
{
    public interface IProfileImageRepository: IGenericRepository<ProfileMedia , ProfileMediaParameters>
    {
        Task<bool> UserHasMainProfileAsync(string userId);
    }
}