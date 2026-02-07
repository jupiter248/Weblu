using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.About
{
    public class AboutUs : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string SubTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Vision { get; set; } = default!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? HeadImageUrl { get; set; }
        public string? HeadImageAltText { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    }
}