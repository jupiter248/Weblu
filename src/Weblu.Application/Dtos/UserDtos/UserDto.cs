using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.TokenDtos;

namespace Weblu.Application.Dtos.UserDtos
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public required string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        // public List<RefreshTokenDto>? RefreshTokens { get; set; }
    }
}