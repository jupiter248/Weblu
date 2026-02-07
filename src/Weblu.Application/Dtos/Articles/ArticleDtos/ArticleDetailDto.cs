using Weblu.Application.Dtos.Articles.ArticleDtos.ArticleImageDtos;

namespace Weblu.Application.Dtos.Articles.ArticleDtos
{
    public class ArticleDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
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
        public List<ArticleImageDto>? ArticleImages { get; set; }
    }
}