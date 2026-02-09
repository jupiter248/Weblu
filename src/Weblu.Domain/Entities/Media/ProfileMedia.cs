using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Domain.Entities.Media
{
    public class ProfileMedia : Media
    {
        // Required properties

        public int Width { get; set; }
        public int Height { get; set; }
        public long FileSize { get; set; }
        public bool IsMain { get; set; }
        // Relationships
        public string OwnerId { get; set; } = default!;
        public ProfileMediaType OwnerType { get; set; } // User , Writer
    }
}