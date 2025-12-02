using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Domain.Entities.Common
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Article> Articles { get; set; } = new List<Article>();

    }
}