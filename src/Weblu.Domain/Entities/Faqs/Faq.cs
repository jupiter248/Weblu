using Weblu.Domain.Entities.Common;
using Weblu.Domain.Errors.FAQs;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.FAQs
{
    public class FAQ : BaseEntity
    {
        // Required properties
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        // Publishing info
        public bool IsPublished { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        // Relationships
        public int CategoryId { get; set; }
        public FAQCategory Category { get; set; } = default!;

        public void Publish()
        {
            if (IsPublished) throw new DomainException(FAQErrorCodes.AlreadyPublished, 409);
            IsPublished = true;
            PublishedAt = DateTimeOffset.Now;
        }
        public void Unpublish()
        {
            if (!IsPublished) throw new DomainException(FAQErrorCodes.DidNotPublish, 409);
            IsPublished = false;
            PublishedAt = null;
        }

    }
}