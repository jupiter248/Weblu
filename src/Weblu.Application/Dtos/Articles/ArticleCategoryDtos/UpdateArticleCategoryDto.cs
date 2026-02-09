namespace Weblu.Application.Dtos.Articles.ArticleCategoryDtos
{
    public class UpdateArticleCategoryDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}