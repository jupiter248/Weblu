using Microsoft.AspNetCore.Http;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Dtos.MediaDtos
{
    public class MediaUploaderDto
    {
        public string FileName { get; set; } = string.Empty;
        public IFormFile Media { get; set; } = default!;
        public required MediaType MediaType { get; set; } // like picture , video and ...
    }
}