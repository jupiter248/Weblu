namespace Weblu.Application.Dtos.Articles.ArticleDtos
{
    public class AddArticleDto
    {
        public string Title { get; set; } = default!;
        public string? BelowTitle { get; set; }
        public string Text { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public bool IsPublished { get; set; } = false;
        public int CategoryId { get; set; }

    }
}