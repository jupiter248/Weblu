using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Services
{
    public class ServiceImage : BaseEntity
    {
        // Required properties
        public bool IsThumbnail { get; set; }
        // Relationships
        public int ServiceId { get; set; }
        public Service Service { get; set; } = default!;
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = default!;

    }
}