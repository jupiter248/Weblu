using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities.Faqs
{
    public class FaqCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!; 
        public string? Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Faq> Faqs { get; set; } = new List<Faq>();
    }
}