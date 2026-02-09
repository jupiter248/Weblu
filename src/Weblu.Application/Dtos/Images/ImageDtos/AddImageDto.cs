using Microsoft.AspNetCore.Http;

namespace Weblu.Application.DTOs.Images.ImageDTOs
{
    public class AddImageDTO
    {
        public IFormFile Image { get; set; } = default!;
        public string? AltText { get; set; }
    }
}