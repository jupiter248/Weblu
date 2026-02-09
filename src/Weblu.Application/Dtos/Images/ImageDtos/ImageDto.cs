namespace Weblu.Application.Dtos.Images.ImageDtos
{
    public class ImageDto
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