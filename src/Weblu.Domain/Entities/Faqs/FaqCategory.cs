using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.FAQs
{
    public class FAQCategory : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<FAQ> FAQs { get; set; } = new List<FAQ>();
    }
}