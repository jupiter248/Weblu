using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.FAQs
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTimeOffset? ActivatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public int CategoryId { get; set; }
        public FAQCategory Category { get; set; } = default!;
    }
}