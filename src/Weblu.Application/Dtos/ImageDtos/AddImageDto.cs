using Microsoft.AspNetCore.Http;

namespace Weblu.Application.Dtos.ImageDtos
{
    public class AddImageDto
    {
        public string FileName { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = default!;
        public string? AltText { get; set; }
    }
}