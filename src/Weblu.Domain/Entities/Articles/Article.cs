using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities.Articles
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? AboveTitle { get; set; }
        public string? BelowTitle { get; set; }
        public string Slug { get; set; } = default!;
        public string Context { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int ViewCount { get; set; } = 0;
        public bool IsPublished { get; set; } = false;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public int CategoryId { get; set; }
        public ArticleCategory Category { get; set; } = default!;
        public List<ArticleImage> ArticleImages { get; set; } = new List<ArticleImage>();

    }
}