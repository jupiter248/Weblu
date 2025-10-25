using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Dtos.ProfileDtos
{
    public class AddProfileDto
    {
        public required IFormFile Image { get; set; }
        public string? AltText { get; set; }
        public required string OwnerId { get; set; }
        public ProfileMediaType OwnerType { get; set; }
        public bool IsMain { get; set; }

    }
}