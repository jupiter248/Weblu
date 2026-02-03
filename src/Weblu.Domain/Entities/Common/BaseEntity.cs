using System.ComponentModel.DataAnnotations;

namespace Weblu.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid PublicId { get; private set; }
        protected BaseEntity()
        {
            PublicId = Guid.NewGuid();
        }
    }
}