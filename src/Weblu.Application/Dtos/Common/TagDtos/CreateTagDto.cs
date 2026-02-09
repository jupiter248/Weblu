namespace Weblu.Application.DTOs.Common.TagDTOs
{
    public class CreateTagDTO
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}