namespace Weblu.Application.DTOs.Articles.ArticleCategoryDTOs
{
    public class CreateArticleCategoryDTO
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}