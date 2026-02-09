using Microsoft.AspNetCore.Http;

namespace Weblu.Application.Dtos.Images.ImageDtos
{
    public class AddImageDto
    {
        public IFormFile Image { get; set; } = default!;
        public string? AltText { get; set; }
    }
}