using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Articles
{
    public class Article : BaseEntity
    {

        public string Title { get; set; } = default!;
        public string? BelowTitle { get; set; }
        public string Slug { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int ViewCount { get; set; } = 0;
        public bool IsPublished { get; set; } = false;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public int CategoryId { get; set; }
        public ArticleCategory Category { get; set; } = default!;
        public List<ArticleImage> ArticleImages { get; set; } = new List<ArticleImage>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<ArticleLike> ArticleLikes { get; set; } = new List<ArticleLike>();
        public List<Tag> Tags { get; set; } = new List<Tag>();


    }
}