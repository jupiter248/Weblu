namespace Weblu.Application.DTOs.FAQs.FAQCategoryDTOs
{
    public class CreateFAQCategoryDTO
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}