namespace Weblu.Application.Dtos.Articles.ArticleDtos
{
    public class UpdateArticleDto
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