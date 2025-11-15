using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ServiceDtos
{
    public class ServiceSummaryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string ShortDescription { get; set; }
        public bool IsActive { get; set; }
        public string? ThumbnailPictureUrl { get; set; }
    }
}