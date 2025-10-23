using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.ProfileDtos
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public string? AltText { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Size { get; set; }
        public required string AddedAt { get; set; }
    }
}