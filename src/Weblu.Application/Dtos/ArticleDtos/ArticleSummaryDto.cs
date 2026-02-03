using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ArticleDtos
{
    public class ArticleSummaryDto
    {
        public Guid PublicId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public bool IsPublished { get; set; } = false;
        public string? ThumbnailPictureUrl { get; set; }
    }
}