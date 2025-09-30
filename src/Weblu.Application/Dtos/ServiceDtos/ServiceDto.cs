using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ServiceDtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public TimeSpan BaseDuration { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? ActivatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    }
}