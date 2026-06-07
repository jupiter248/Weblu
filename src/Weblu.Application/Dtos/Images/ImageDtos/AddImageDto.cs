namespace Weblu.Application.DTOs.Images.ImageDTOs
{
    public class UploadImageDTO
    {
        public Stream Image { get; set; } = default!;
        public string? AltText { get; set; }
    }
}