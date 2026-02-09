using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Common.Tags
{
    public class Tag : BaseEntity
    {
        // Required properties
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        // Relationships
        public List<Article> Articles { get; set; } = new();

    }
}