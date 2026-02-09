using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.About
{
    public class SocialMedia : BaseEntity
    {
        // Required properties
        public string Name { get; set; } = default!;
        public string? Link { get; set; }
        public string? IconUrl { get; set; }
        public string? IconAltText { get; set; }
    }
}