using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Media
{
    public abstract class Media : BaseEntity
    {
        // Required properties
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? AltText { get; set; }
    }
}