namespace Weblu.Api.Models;

public class AddImageRequest
{
    public IFormFile Image { get; set; } = default!;
    public string? AltText { get; set; }
}