using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.About
{
    public class AboutUs : BaseEntity
    {
        // Required properties
        public string Title { get; set; } = default!;
        public string? SubTitle { get; set; }
        public string Description { get; set; } = default!;
        public string? Vision { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? HeadImageUrl { get; set; }
        public string? HeadImageAltText { get; set; }
    }
}