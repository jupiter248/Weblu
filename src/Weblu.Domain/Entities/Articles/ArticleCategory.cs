using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleCategory : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}