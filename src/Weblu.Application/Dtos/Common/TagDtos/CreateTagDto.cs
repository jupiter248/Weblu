namespace Weblu.Application.Dtos.Common.TagDtos
{
    public class CreateTagDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}