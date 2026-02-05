using System.ComponentModel.DataAnnotations;

namespace Weblu.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid GuidId { get; private set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public virtual void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTimeOffset.Now;
        }
        protected BaseEntity()
        {
            GuidId = Guid.NewGuid();
        }
    }
}