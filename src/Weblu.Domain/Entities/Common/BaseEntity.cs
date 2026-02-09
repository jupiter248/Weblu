using System.ComponentModel.DataAnnotations;

namespace Weblu.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public Guid GuidId { get; private set; }
        public bool IsDeleted { get; protected set; } = false;
        public bool IsUpdated { get; protected set; } = false;
        public DateTimeOffset? DeletedAt { get; protected set; }
        public DateTimeOffset? UpdatedAt { get; protected set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public virtual void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTimeOffset.Now;
        }
        protected BaseEntity()
        {
            GuidId = Guid.NewGuid();
            CreatedAt = DateTimeOffset.Now;
        }
        public virtual void MarkUpdated()
        {
            IsUpdated = true;
            UpdatedAt = DateTimeOffset.Now;
        }

    }
}