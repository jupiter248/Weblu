using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ImageDtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string AltText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public required string AddedAt { get; set; }
    }
}