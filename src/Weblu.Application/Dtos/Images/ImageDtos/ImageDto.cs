namespace Weblu.Application.DTOs.Images.ImageDTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public string? AltText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public required string AddedAt { get; set; }
    }
}