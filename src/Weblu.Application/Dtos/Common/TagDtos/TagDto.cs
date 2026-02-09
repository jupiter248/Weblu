namespace Weblu.Application.DTOs.Common.TagDTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedAt { get; set; } = default!;
    }
}