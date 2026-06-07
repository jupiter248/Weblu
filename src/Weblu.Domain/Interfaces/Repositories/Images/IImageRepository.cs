using Weblu.Domain.Entities.Media;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Parameters.Images;

namespace Weblu.Domain.Interfaces.Repositories.Images
{
    public interface IImageRepository : IGenericRepository<ImageMedia, ImageParameters>
    {
    }
}