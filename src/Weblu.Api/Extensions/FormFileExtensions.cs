using Weblu.Api.Models;
using Weblu.Application.DTOs.Images.ImageDTOs;

namespace Weblu.Api.Extensions;

public static class FormFileExtensions
{
    public static UploadImageDTO ToAddImageDTO(this AddImageRequest imageRequest) => new()
    {
        Image = imageRequest.Image.OpenReadStream(),
        AltText = imageRequest.AltText,
    };
}