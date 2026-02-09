using Microsoft.AspNetCore.Http;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.DTOs.Images.MediaDTOs
{
    public class MediaUploaderDTO
    {
        public string FileName { get; set; } = string.Empty;
        public IFormFile Media { get; set; } = default!;
        public required MediaType MediaType { get; set; } // like picture , video and ...
    }
}