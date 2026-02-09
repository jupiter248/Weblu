namespace Weblu.Application.DTOs.Common.MethodDTOs
{
    public class MethodDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAltText { get; set; }
        public string? UpdatedAt { get; set; }
        public required string CreatedAt { get; set; }
    }
}