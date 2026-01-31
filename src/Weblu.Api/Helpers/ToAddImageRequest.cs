using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ImageDtos;

namespace Weblu.Api.Helpers
{
    public static class FileHelper
    {
        public static Stream ToAddImageRequest(this IFormFile file)
        {
            return file.OpenReadStream();
        }
    }
}