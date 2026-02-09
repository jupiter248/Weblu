using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.FAQs
{
    public class FAQCategory : BaseEntity
    {
        // Required properties
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        // Relationships
        public List<FAQ> FAQs { get; set; } = new();
    }
}