using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Weblu.Application.Dtos.MethodDtos
{
    public class UpdateMethodImageDto
    {
        public required IFormFile Image { get; set; }
        public string? AltText { get; set; }
    }
}