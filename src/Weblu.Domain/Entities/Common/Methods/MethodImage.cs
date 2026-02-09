using Weblu.Domain.Entities.Media;

namespace Weblu.Domain.Entities.Common.Methods
{
    public class MethodImage : BaseEntity
    {
        // Relationships
        public int ImageId { get; set; }
        public ImageMedia Image { get; set; } = default!;
        public int MethodId { get; set; }
        public Method Method { get; set; } = default!;
    }
}