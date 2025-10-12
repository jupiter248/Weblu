using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Common.Dtos
{
    public class MediumUploaderDto
    {
        public required IFormFile Medium { get; set; }
        public required MediumType MediumType { get; set; } // like picture , video and ...
        public required MediumParentEntityType MediumParentEntityType { get; set; } // Like service , profile and ...

    }
}