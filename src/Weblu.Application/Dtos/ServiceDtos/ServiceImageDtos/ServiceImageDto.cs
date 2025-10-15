using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ImageDtos;

namespace Weblu.Application.Dtos.ServiceDtos.ServiceImageDtos
{
    public class ServiceImageDto : ImageDto
    {
        public bool IsThumbnail { get; set; }
    }
}