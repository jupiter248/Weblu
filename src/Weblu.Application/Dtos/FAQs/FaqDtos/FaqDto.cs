namespace Weblu.Application.Dtos.FAQs.FAQDtos
{
    public class FAQDto
    {
        public int Id { get; set; }
        public string Question { get; set; } = default!;
        public string Answer { get; set; } = default!;
        public bool IsPublished { get; set; }
        public string? PublishedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}