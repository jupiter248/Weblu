using Weblu.Application.DTOs.Articles.ArticleDTOs.ArticleImageDTOs;
using Weblu.Domain.Events.Articles;

namespace Weblu.Application.DTOs.Articles.ArticleDTOs
{
    public class ArticleDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public int ReadingTimeMinutes { get; set; }
        public string? AboveTitle { get; set; }
        public string? BelowTitle { get; set; }
        public string Slug { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public bool IsPublished { get; set; } = false;
        public string? UpdatedAt { get; set; }
        public string? PublishedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
        public List<ArticleImageDTO>? ArticleImages { get; set; }
    }
}