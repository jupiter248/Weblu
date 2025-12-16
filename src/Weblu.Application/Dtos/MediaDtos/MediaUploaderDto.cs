using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Dtos.MediaDtos
{
    public class MediaUploaderDto
    {
        public required IFormFile Media { get; set; }
        public required MediaType MediaType { get; set; } // like picture , video and ...
    }
}