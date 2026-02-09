namespace Weblu.Application.Dtos.FAQs.FAQCategoryDtos
{
    public class CreateFAQCategoryDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}