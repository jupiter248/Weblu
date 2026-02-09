namespace Weblu.Application.Dtos.FAQs.FAQCategoryDtos
{
    public class FAQCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
    }
}