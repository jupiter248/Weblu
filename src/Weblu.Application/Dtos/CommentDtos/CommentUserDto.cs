using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Application.Dtos.CommentDtos
{
    public class CommentUserDto
    {
        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string? UserProfileUrl { get; set; }
        public string? UserProfileAltText { get; set; }
    }
}