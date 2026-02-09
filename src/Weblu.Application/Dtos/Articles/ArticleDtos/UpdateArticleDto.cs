namespace Weblu.Application.DTOs.Articles.ArticleDTOs
{
    public class UpdateArticleDTO
    {
        public string Title { get; set; } = default!;
        public int ReadingTimeMinutes { get; set; }
        public string? BelowTitle { get; set; }
        public string Text { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}